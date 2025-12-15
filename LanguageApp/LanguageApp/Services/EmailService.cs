

namespace LanguageApp.Services
{
    public class EmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("=== EMAIL SEND START ===");
            _logger.LogInformation("To: {Email}", email);
            _logger.LogInformation("Subject: {Subject}", subject);

            try
            {
                // 1. Get API Key from MailSettings
                var apiKey = _configuration["MailSettings:SendGridApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    _logger.LogError("❌ SendGrid API Key is NULL or EMPTY!");
                    throw new InvalidOperationException("SendGrid API Key is not configured");
                }
                _logger.LogInformation("✓ API Key found (length: {Length})", apiKey.Length);

                // 2. Get From email and name
                var fromEmail = _configuration["MailSettings:From"];
                var fromName = _configuration["MailSettings:SenderName"];
                if (string.IsNullOrEmpty(fromEmail))
                {
                    _logger.LogError("❌ SendGrid FromEmail is not configured!");
                    throw new InvalidOperationException("SendGrid FromEmail is not configured");
                }
                _logger.LogInformation("✓ From: {FromName} <{FromEmail}>", fromName, fromEmail);

                // 3. Create SendGrid client
                _logger.LogInformation("Creating SendGrid client...");
                var client = new SendGridClient(apiKey);

                // 4. Create email message
                _logger.LogInformation("Creating email message...");
                var from = new EmailAddress(fromEmail, fromName);
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent: htmlMessage);

                // 5. Send email
                _logger.LogInformation("Sending email via SendGrid...");
                var response = await client.SendEmailAsync(msg);
                _logger.LogInformation("SendGrid Response Status: {StatusCode}", response.StatusCode);

                // 6. Check response
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                    response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation("✅✅✅ EMAIL SENT SUCCESSFULLY to {Email}", email);
                }
                else
                {
                    var body = await response.Body.ReadAsStringAsync();
                    _logger.LogError("❌ SendGrid returned non-success status");
                    _logger.LogError("Status Code: {StatusCode}", response.StatusCode);
                    _logger.LogError("Response Body: {Body}", body);

                    throw new Exception($"SendGrid error: {response.StatusCode} - {body}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌❌❌ FAILED TO SEND EMAIL to {Email}", email);
                _logger.LogError("Exception Type: {Type}", ex.GetType().Name);
                _logger.LogError("Exception Message: {Message}", ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.LogError("Inner Exception: {InnerMessage}", ex.InnerException.Message);
                }
                throw;
            }
        }



    }
}
