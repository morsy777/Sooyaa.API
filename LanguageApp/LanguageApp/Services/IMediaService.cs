namespace LanguageApp.Services
{
    public interface IMediaService
    {
        Task<string> UploadAsync(IFormFile file, string folder);

    }
}
