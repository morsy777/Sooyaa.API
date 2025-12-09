using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;
using Microsoft.EntityFrameworkCore;

namespace LanguageApp.Application.Bussiness
{
    public class homeService: IhomeService
    {
        private readonly ApplicationDbContext _dbContext;

        public homeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<homePageDTO> GetHomePageDataAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
