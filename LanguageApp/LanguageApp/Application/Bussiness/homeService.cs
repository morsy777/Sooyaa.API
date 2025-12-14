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

        public async Task<homePageDTO> GetHomePageDataAsync(string userId,int lanId,CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.UserStreak)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
                return null!;

            var userLanguageInfo = await _dbContext.UserLanguages
                .AsNoTracking()
                .Include(ul => ul.Language)
                .Include(ul => ul.Level)
                .Where(ul => ul.UserId == userId && ul.LanguageId == lanId)
                .Select(ul => new
                {
                    ul.Language.Name,
                    ul.Language.Image,
                    LevelName = ul.Level.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            var categories = await _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.LanguageId == lanId)
                .Select(c => c.Name)
                .ToListAsync(cancellationToken);

            var lessonsQuery = _dbContext.Lessons
                .AsNoTracking()
                .Where(l =>
                    _dbContext.Chapters.Any(ch => ch.Id == l.ChapterId && ch.Level.LanguageId == lanId) ||
                    _dbContext.Categories.Any(c => c.Id == l.CategoryId && c.LanguageId == lanId));

            var lessonIds = await lessonsQuery
                .Select(l => l.Id)
                .ToListAsync(cancellationToken);

            var completedLessonsCount = await _dbContext.UserProgresses
                .AsNoTracking()
                .Where(up =>
                    up.UserId == userId &&
                    up.IsCompleted &&
                    lessonIds.Contains(up.LessonId))
                .CountAsync(cancellationToken);

            return new homePageDTO
            {
                LanName = userLanguageInfo?.Name ?? "Unknown",
                LanFlag = userLanguageInfo?.Image ?? "",
                userLevel = userLanguageInfo?.LevelName ?? "Beginner",
                streakScore = user.UserStreak?.CurrentStreak ?? 0,
                Categories = categories,
                TotalLessons = lessonIds.Count,
                CompletedLesson = completedLessonsCount
            };
        }



    }
}
