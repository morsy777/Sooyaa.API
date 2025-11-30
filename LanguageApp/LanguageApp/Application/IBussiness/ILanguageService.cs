namespace LanguageApp.Application.IBussiness
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
    }
}
