using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguagesDTO>> GetAllLanguagesAsync();
    }
}
