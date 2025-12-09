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

        public async Task<homePageDTO> GetHomePageDataAsync(string userId, int LanId, CancellationToken cancellationToken)
        {
            // Get user + streak
            var user = await _dbContext.Users
                .Include(u => u.UserStreak)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
                return null!;

            // Get user language name based on LanId
            var userLanguage = await _dbContext.UserLanguages
                .AsNoTracking()
                .Include(ul => ul.Language)
                .Where(ul => ul.UserId == userId && ul.LanguageId == LanId)
                .Select(ul => ul.Language.Name)
                .FirstOrDefaultAsync(cancellationToken);

            // Get user level based on LanId
            var userLevel = await _dbContext.UserLanguages
                .AsNoTracking()
                .Include(ul => ul.Level)
                .Where(ul => ul.UserId == userId && ul.LanguageId == LanId)
                .Select(ul => ul.Level.Name)
                .FirstOrDefaultAsync(cancellationToken);

            // Streak (already included above)
            int streakScore = user.UserStreak?.CurrentStreak ?? 0;

            // Categories
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .Select(c => c.Name)
                .ToListAsync(cancellationToken);

            // Build DTO
            var homePageData = new homePageDTO
            {
                LanName = userLanguage ?? "Unknown",
                streakScore = streakScore,
                userLevel = userLevel ?? "Beginner",
                Categories = categories
            };

            return homePageData;
        }



    }
}
