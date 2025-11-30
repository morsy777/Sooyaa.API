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

            var user = await _dbContext.Users
                .Include(u => u.UserStreak)
                .Include(u => u.SelectedLevel)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null)
                return null!;


            var categories = await _dbContext.Categories
                .Select(c => c.Name)
                .ToListAsync(cancellationToken);

            var homePage = user.Adapt<homePageDTO>();
            homePage.Categories = categories;


            return homePage;
        }


    }
}
