
namespace LanguageApp.Application.IBussiness
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDTO>> GetQuestionsByLessonAsync(int lessonId,CancellationToken cancellationToken);

    }
}
