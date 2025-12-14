using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace LanguageApp.Application.Bussiness
{
    public class LanguageService(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) : ILanguageService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        private readonly string _langImagesDirName = "langImages";
        private readonly string _langImagesDirPath = $"{webHostEnvironment.WebRootPath}/langImages";


        public async Task<IEnumerable<LanguagesDTO>> GetAllLanguagesAsync()
        {
            var languages = await _dbContext.Languages.ToListAsync();

            return languages.Adapt<IEnumerable<LanguagesDTO>>();
        }

        public async Task<string?> GetLanguageImageAsync(int languageId, HttpRequest request)
        {
            var imageFileName = await _dbContext.Languages
                .Where(l => l.Id == languageId)
                .Select(l => l.Image)
                .SingleOrDefaultAsync();

            if (string.IsNullOrEmpty(imageFileName))
                return null;

            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}/{imageFileName}";

        }

        public async Task UploadLanguageImageAsync(UploadLanguageImageRequestDto request)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);

            var imgFullPath = Path.Combine(_langImagesDirPath, uniqueFileName);

            using var stream = File.Create(imgFullPath);
            await request.Image.CopyToAsync(stream);

            var imgRelativePath = Path.Combine(_langImagesDirName, uniqueFileName);
            imgRelativePath = imgRelativePath.Replace("\\", "/");

            await _dbContext.Languages
                .Where(l => l.Id == request.languageId)
                .ExecuteUpdateAsync(setters =>
                    setters
                        .SetProperty(x => x.Image, imgRelativePath)
                );
        }
    }
}
