namespace LanguageApp.Services;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider,
    ILogger<AuthService> logger,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    IWebHostEnvironment env) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IConfiguration _configuration = configuration;
    private readonly IWebHostEnvironment _env = env;
    private readonly int _refreshTokenExpireDays = 14;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password,
        CancellationToken cancellation = default)
    {
        // Check user by using email
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (result.Succeeded)
        {
            // Generate JWT Token
            var (token, expiresIn) = _jwtProvider.GenerateToken(user);

            // Call Refresh Token Generator Function
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpireDays);

            // Add Refresh Token to DB
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            // Return Token & Refresh Token
            var response = new AuthResponse(
                    user.Id,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    token,
                    expiresIn,
                    refreshToken,
                    refreshTokenExpiration
            );

            return Result.Success(response);
        }

        return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (userRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

        // Revoke the old refresh token
        userRefreshToken.RevokedOn = DateTime.UtcNow;

        // Generate New JWT Token
        var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);

        // Call Refresh Token Generator Function to Generate New refresh token
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpireDays);

        // Add Refresh Token to DB
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        // Return Token & Refresh Token
        var response = new AuthResponse(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                newToken,
                expiresIn,
                newRefreshToken,
                refreshTokenExpiration
        );

        return Result.Success<AuthResponse>(response);
    }

    public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (userRefreshToken is null)
            return Result.Failure(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }

    public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellation = default)
    {
        try
        {
            // 1️⃣ Check if email exists
            var emailIsExist = await _userManager.Users
                .AnyAsync(x => x.Email == request.Email, cancellation);

            if (emailIsExist)
                return Result.Failure<AuthResponse>(UserErrors.DuplicatedEmail);

            // 2️⃣ Map request to ApplicationUser
            var user = request.Adapt<ApplicationUser>();

            // 3️⃣ Create user
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result.Failure(new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.Status401Unauthorized));
            }

            // 4️⃣ Generate email confirmation token
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Email confirmation code generated for user {UserId}", user.Id);

            // 5️⃣ Try sending confirmation email (DO NOT break registration)
            try
            {
                await SendConfirmationEmail(user, code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to send confirmation email to {Email}",
                    user.Email);

                // Optional: return success but tell frontend email failed
                return Result.Success("User created, but email could not be sent.");
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            // 6️⃣ Catch unexpected errors (DB, config, Hangfire, SMTP)
            _logger.LogCritical(ex,
                "Unexpected error during registration for {Email}",
                request.Email);

            return Result.Failure(new Error(
                "RegisterFailed",
                "An unexpected error occurred during registration.",
                StatusCodes.Status500InternalServerError));
        }
    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        _logger.LogInformation("=== EMAIL CONFIRMATION DEBUG START ===");
        _logger.LogInformation("UserId: {UserId}", request.UserId);
        _logger.LogInformation("Code length: {Length}", request.Code?.Length ?? 0);
        _logger.LogInformation("Code (first 50 chars): {Code}",
            request.Code?.Substring(0, Math.Min(50, request.Code?.Length ?? 50)) ?? "NULL");

        // 1. Find user by ID
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            _logger.LogError("❌ STEP 1 FAILED: User not found with ID: {UserId}", request.UserId);
            return Result.Failure(UserErrors.InvalidCode);
        }

        _logger.LogInformation("✓ STEP 1 PASSED: User found - Email: {Email}, EmailConfirmed: {EmailConfirmed}",
            user.Email, user.EmailConfirmed);

        // 2. Check if email is already confirmed
        if (user.EmailConfirmed)
        {
            _logger.LogWarning("❌ STEP 2 FAILED: Email already confirmed for user {UserId}", user.Id);
            return Result.Failure(UserErrors.DuplicatedConfirmation);
        }

        _logger.LogInformation("✓ STEP 2 PASSED: Email not yet confirmed");

        // 3. Decode the Base64Url encoded token
        string code;
        try
        {
            _logger.LogInformation("STEP 3: Attempting to decode token...");
            var codeBytes = WebEncoders.Base64UrlDecode(request.Code);
            code = Encoding.UTF8.GetString(codeBytes);
            _logger.LogInformation("✓ STEP 3 PASSED: Token decoded. Decoded length: {Length}", code.Length);
            _logger.LogInformation("Decoded token (first 50 chars): {Code}", code.Substring(0, Math.Min(50, code.Length)));
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "❌ STEP 3 FAILED: Failed to decode token");
            return Result.Failure(UserErrors.InvalidCode);
        }

        // 4. Confirm email using UserManager
        _logger.LogInformation("STEP 4: Calling UserManager.ConfirmEmailAsync...");
        var result = await _userManager.ConfirmEmailAsync(user, code);

        _logger.LogInformation("STEP 4 Result: Succeeded={Succeeded}, ErrorCount={ErrorCount}",
            result.Succeeded, result.Errors.Count());

        // 5. Return result
        if (result.Succeeded)
        {
            _logger.LogInformation("✓✓✓ EMAIL CONFIRMED SUCCESSFULLY for user {UserId} ({Email})",
                user.Id, user.Email);
            return Result.Success();
        }

        // Log all Identity errors for debugging
        _logger.LogError("❌ STEP 4 FAILED: Identity rejected the token. Errors:");
        foreach (var error in result.Errors)
        {
            _logger.LogError("  - ERROR CODE: {Code}, DESCRIPTION: {Description}", error.Code, error.Description);
        }

        var firstError = result.Errors.First();
        return Result.Failure(new Error(
            firstError.Code,
            firstError.Description,
            StatusCodes.Status400BadRequest));
    }

    public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
    {
        _logger.LogInformation("Resend confirmation email requested for {Email}", request.Email);

        // 1. Find user by email
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
        {
            // Don't reveal if user exists or not for security
            _logger.LogInformation("User not found for email {Email}", request.Email);
            return Result.Success();
        }

        // 2. Check if email already confirmed
        if (user.EmailConfirmed)
        {
            _logger.LogInformation("Email already confirmed for {Email}", request.Email);
            return Result.Failure(UserErrors.DuplicatedConfirmation);
        }

        // 3. Generate new confirmation token
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        _logger.LogInformation("New confirmation token generated for user {UserId}", user.Id);

        // 4. Send confirmation email
        try
        {
            await SendConfirmationEmail(user, code);
            _logger.LogInformation("Confirmation email resent to {Email}", user.Email);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to resend confirmation email to {Email}", user.Email);
            return Result.Failure(new Error(
                "EmailSendFailed",
                "Failed to send confirmation email. Please try again.",
                StatusCodes.Status500InternalServerError));
        }
    }

    public async Task<Result> SendResetPasswordCodeAsync(string email)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        _logger.LogInformation("Password reset code generated for user {UserId}", user.Id);

        try
        {
            await SendResetPasswordEmail(user, code);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send password reset email to {Email}", user.Email);
            return Result.Failure(new Error(
                "EmailSendFailed",
                "Failed to send password reset email.",
                StatusCodes.Status500InternalServerError));
        }
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Failure(UserErrors.InvalidCode);

        IdentityResult result;

        try
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
        }

        if (result.Succeeded)
        {
            _logger.LogInformation("Password reset successful for user {UserId}", user.Id);
            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
    }

    // Generate Refresh Token
    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private async Task SendConfirmationEmail(ApplicationUser user, string code)
    {
        var baseUrl = _configuration["AppUrl"]?.TrimEnd('/');

        if (string.IsNullOrEmpty(baseUrl))
        {
            _logger.LogError("AppUrl configuration is missing");
            throw new InvalidOperationException("AppUrl is not configured in appsettings.json");
        }

        // Build verification URL
        var verifyUrl = $"{baseUrl}/auth/confirm-email?userId={user.Id}&code={code}";

        _logger.LogInformation("Sending confirmation email to {Email}", user.Email);

        // Use instance-based EmailBodyBuilder with IWebHostEnvironment
        var emailBodyBuilder = new EmailBodyBuilder(_env); // _env must be injected in the controller
        var emailBody = emailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
    {
        { "{{name}}", user.FirstName },
        { "{{action_url}}", verifyUrl }
    });

        // Enqueue email via Hangfire
        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
            user.Email!,
            "Sooyaa App: Email Confirmation",
            emailBody));

        await Task.CompletedTask;
    }

    private async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
        var baseUrl = _configuration["AppUrl"]?.TrimEnd('/');

        if (string.IsNullOrEmpty(baseUrl))
        {
            _logger.LogError("AppUrl configuration is missing");
            throw new InvalidOperationException("AppUrl is not configured in appsettings.json");
        }

        // URL to static reset password page
        var resetUrl =
            $"{baseUrl}/password/reset-password.html?email={user.Email}&code={code}";

        _logger.LogInformation("Sending reset password email to {Email}", user.Email);

        // ✅ Use instance-based EmailBodyBuilder (same as confirmation)
        var emailBodyBuilder = new EmailBodyBuilder(_env);

        var emailBody = emailBodyBuilder.GenerateEmailBody(
            "ForgetPassword",
            new Dictionary<string, string>
            {
            { "{{name}}", user.FirstName },
            { "{{action_url}}", resetUrl }
            });

        // Enqueue via Hangfire
        BackgroundJob.Enqueue(() =>
            _emailSender.SendEmailAsync(
                user.Email!,
                "Sooyaa App: Reset Password",
                emailBody));

        await Task.CompletedTask;
    }


}