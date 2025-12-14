
namespace LanguageApp.Application.Bussiness
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsByLessonAsync(int lessonId,CancellationToken cancellationToken)
        {
            var questions = await _dbContext.Questions
                .AsNoTracking()
                .Where(q => q.LessonId == lessonId)
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    LessonId = q.LessonId,
                    QuestionText = q.QuestionText,
                    Explanation = q.Explanation, 
                    Answers = q.Answers
                        .Select(a => new getAnswerforQuestionDTO
                        {
                            Id = a.Id,
                            AnswerText = a.AnswerText
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            return questions;
        }




    }
}
