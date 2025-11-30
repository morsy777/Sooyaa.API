using LanguageApp.Application.IBussiness;

namespace LanguageApp.Application.Bussiness
{
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationDbContext _dbContext;

        public LanguageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _dbContext.Languages.ToListAsync();
        }

    }
}
