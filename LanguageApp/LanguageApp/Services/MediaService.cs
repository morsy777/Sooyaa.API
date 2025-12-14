using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class MediaService : IMediaService
{
    private readonly Cloudinary _cloudinary;

    public MediaService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> UploadAsync(IFormFile file, string folder)
    {
        var uploadParams = new RawUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folder
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        return result.SecureUrl.ToString();
    }


}
