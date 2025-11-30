using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDTO>> GetAllLessonsAync(int categoryId,int chapterId,CancellationToken cancellationToken);
    }
}
