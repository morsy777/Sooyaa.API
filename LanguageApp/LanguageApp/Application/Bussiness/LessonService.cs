using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;

namespace LanguageApp.Application.Bussiness
{
    public class LessonService: ILessonService
    {
        private readonly ApplicationDbContext _dbContext;

        public LessonService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LessonDTO>> GetAllLessonsForLanguageForCategoryAsync(int languageId,int categoryId,CancellationToken cancellationToken)
        {
            var lessons = await _dbContext.Lessons
                .Where(l =>
                    l.Chapter.Level.LanguageId == languageId &&  
                    l.CategoryId == categoryId
                )
                .OrderBy(l => l.Chapter.OrderNumber)
                .ThenBy(l => l.OrderNumber)
                .Select(l => new LessonDTO
                {
                    Id = l.Id,
                    ChapterId = l.ChapterId,
                    CategoryId = l.CategoryId,
                    Title = l.Title,
                    Content = l.Content,
                    OrderNumber = l.OrderNumber
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return lessons;
        }
        public async Task<string> AddLessonToSavedLessonsAsync(SaveLessonRequestDTO dto,CancellationToken cancellationToken)
        {
            if (!await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == dto.userId, cancellationToken))
                return "User not found!";

            bool lessonExists = await _dbContext.Lessons
                .AsNoTracking()
                .AnyAsync(l =>
                    l.Id == dto.lessonId &&
                    l.Chapter.Level.LanguageId == dto.LanId,
                    cancellationToken);

            if (!lessonExists)
                return "Lesson not found for this language!";

            bool alreadySaved = await _dbContext.savedLessons
                .AsNoTracking()
                .AnyAsync(sl =>
                    sl.UserId == dto.userId &&
                    sl.LessonId == dto.lessonId &&
                    sl.LanguageId == dto.LanId,
                    cancellationToken);

            if (alreadySaved)
                return "Lesson already saved!";

            await _dbContext.savedLessons.AddAsync(new SavedLesson
            {
                UserId = dto.userId,
                LessonId = dto.lessonId,
                LanguageId = dto.LanId
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Lesson saved successfully!";
        }
        public async Task<string> RemoveLessonFromSavedLessonsAsync(SaveLessonRequestDTO dto,CancellationToken cancellationToken)
        {
            var savedLesson = await _dbContext.savedLessons
                .FirstOrDefaultAsync(sl =>
                    sl.UserId == dto.userId &&
                    sl.LessonId == dto.lessonId &&
                    sl.LanguageId == dto.LanId,
                    cancellationToken);

            if (savedLesson == null)
                return "Saved lesson not found!";

            _dbContext.savedLessons.Remove(savedLesson);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Lesson removed successfully!";
        }
        public async Task<IEnumerable<SavedLessonResponseDTO>> GetSavedLessonsForUserForLanguageAsync(string userId,int languageId,CancellationToken cancellationToken)
        {
            var savedLessons = await _dbContext.savedLessons
                .Where(sl => sl.UserId == userId && sl.LanguageId == languageId)
                .Include(sl => sl.Lesson)
                    .ThenInclude(l => l!.Chapter)
                        .ThenInclude(c => c.Level)
                            .ThenInclude(lv => lv.Language)
                .Select(sl => new SavedLessonResponseDTO
                {
                    Id = sl.Lesson!.Id,
                    Title = sl.Lesson.Title,
                    ChapterName = sl.Lesson.Chapter.Name,
                    LevelName = sl.Lesson.Chapter.Level.Name,
                    LanguageName = sl.Lesson.Chapter.Level.Language.Name
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return savedLessons;
        }
        public async Task<LessonDTO> GetLessonByIdAsync(int lessonId,CancellationToken cancellationToken)
        {
            var lesson = await _dbContext.Lessons
                .AsNoTracking()
                .Where(l => l.Id == lessonId)
                .Select(l => new LessonDTO
                {
                    Id = l.Id,
                    CategoryId = l.CategoryId,
                    ChapterId = l.ChapterId,
                    Title = l.Title,
                    Content = l.Content,
                    OrderNumber = l.OrderNumber
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (lesson == null)
                return null!;

            return lesson;
        }




    }
}
