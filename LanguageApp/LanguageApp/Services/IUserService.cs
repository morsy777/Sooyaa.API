namespace LanguageApp.Services;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId, HttpRequest request);
    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    Task<Result> UploadProfileImg(string userId, IFormFile image);
    //Task<Result<ProfileImageResponse>> GetProfileImageAsync(string userId, );
}
