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

        public async Task<IEnumerable<LessonDTO>> GetAllLessonsForLanguageForCategoryAsync(
            int languageId,
            int categoryId,
            CancellationToken cancellationToken)
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
  
        public async Task<string> AddLessonToSavedLessonsAsync(SaveLessonRequestDTO dTO, CancellationToken cancellationToken)
        {
            // Check if user exists
            bool userExists = await _dbContext.Users
                .AnyAsync(u => u.Id == dTO.userId, cancellationToken);

            if (!userExists)
                return "User not found!";

            // Check if lesson exists for the selected language through Chapter -> Level -> Language
            bool lessonExists = await _dbContext.Lessons
                .Include(l => l.Chapter)
                    .ThenInclude(c => c.Level)
                        .ThenInclude(lv => lv.Language)
                .AnyAsync(l =>
                    l.Id == dTO.lessonId &&
                    l.Chapter.Level.Language.Id == dTO.LanId,
                    cancellationToken);

            if (!lessonExists)
                return "Lesson not found for this language!";

            // Check if lesson already saved for this user & language
            bool alreadySaved = await _dbContext.savedLessons
                .AnyAsync(sl =>
                    sl.UserId == dTO.userId &&
                    sl.LessonId == dTO.lessonId &&
                    sl.LanguageId == dTO.LanId,
                    cancellationToken);

            if (alreadySaved)
                return "Lesson already saved!";

            // Save the lesson
            var entity = new SavedLesson
            {
                UserId = dTO.userId,
                LessonId = dTO.lessonId,
                LanguageId = dTO.LanId
            };

            await _dbContext.savedLessons.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Lesson saved successfully!";
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



    }
}
