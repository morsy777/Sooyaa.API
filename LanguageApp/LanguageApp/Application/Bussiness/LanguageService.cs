using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;

namespace LanguageApp.Application.Bussiness
{
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationDbContext _dbContext;

        public LanguageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LanguagesDTO>> GetAllLanguagesAsync()
        {
            var languages = await _dbContext.Languages.ToListAsync();

            return languages.Adapt<IEnumerable<LanguagesDTO>>();
        }

        //public Task<string> GetLanguageImageAsync(int languageId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<UploadLanguageImageRequestDto> UploadLanguageImageAsync(UploadLanguageImageRequestDto request)
        //{
        //    await _dbContext.Languages
        //        .Where(x => x.Id == request.languageId)
        //        .ExecuteUpdateAsync(setter =>
        //            setter.SetProperty(x => x.)
        //        );
        //}
    }
}
