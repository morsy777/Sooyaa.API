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

        public async Task<IEnumerable<LessonDTO>> GetAllLessonsAync(int categoryId, int chapterId,CancellationToken cancellationToken)
        {
            var lessons = await _dbContext.Lessons
                .Where(l => l.CategoryId == categoryId && l.ChapterId == chapterId)
                .ToListAsync(cancellationToken);

            return lessons.Adapt<IEnumerable<LessonDTO>>();

        }


    }
}
