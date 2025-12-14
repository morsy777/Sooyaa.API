using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguagesDTO>> GetAllLanguagesAsync();
        //Task<UploadLanguageImageRequestDto> UploadLanguageImageAsync(UploadLanguageImageRequestDto request);
        //Task<string> GetLanguageImageAsync(int languageId);
    }
}
