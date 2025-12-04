namespace LanguageApp.Contracts.User;

public record UploadProfileImageRequest(
    IFormFile Image    
);