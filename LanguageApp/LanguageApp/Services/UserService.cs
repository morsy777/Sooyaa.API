namespace LanguageApp.Services;

public class UserService(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly string _profileImagesDirName = "profileImages";
    private readonly string _profileImagesDirPath = $"{webHostEnvironment.WebRootPath}/profileImages";


    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId, HttpRequest request)
    {
        var user = await _userManager.Users
            .Where(x => x.Id == userId)
            .ProjectToType<UserProfileResponse>()
            .SingleAsync();

        var baseUrl = $"{request.Scheme}://{request.Host}";

        user = user with { profileImage = $"{baseUrl}/me/get-profile-image" };

        return Result.Success(user);
    }

    public async Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request)
    {
        await _userManager.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(setters =>
                setters
                    .SetProperty(x => x.FirstName, request.FirstName)
                    .SetProperty(x => x.LastName, request.LastName)
            );

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

        if(result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result> UploadProfileImg(string userId, IFormFile image)
    {
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

        var imgFullPath = Path.Combine(_profileImagesDirPath, uniqueFileName);

        using var stream = File.Create(imgFullPath);
        await image.CopyToAsync(stream);

        var imgRelativePath = Path.Combine(_profileImagesDirName, uniqueFileName);
        imgRelativePath = imgRelativePath.Replace("\\", "/");

        await _userManager.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(setters =>
                setters
                    .SetProperty(x => x.profileImage, imgRelativePath)
            );

        return Result.Success();
    }

//    public async Task<Result<ProfileImageResponse>> GetProfileImageAsync(string userId)
//    {
//        var imageUrl = _userManager.Users
//            .Where(x => x.Id == userId)
//            .Select(x => x.profileImage);



//        return Result.Success(imageUrl);
//    }
}
