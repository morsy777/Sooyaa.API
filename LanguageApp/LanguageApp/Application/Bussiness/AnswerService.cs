
namespace LanguageApp.Application.Bussiness
{
    public class AnswerService : IAnswersService
    {
        private readonly ApplicationDbContext _dbContext;

        public AnswerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QuestionResultDTO> CheckAnswerAsync(int questionId,int selectedOptionId,CancellationToken cancellationToken)
        {
            var answer = await _dbContext.Answers
                .AsNoTracking()
                .Where(a => a.Id == selectedOptionId && a.QuestionId == questionId)
                .Select(a => new
                {
                    a.IsCorrect,
                    a.Question.Explanation
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (answer == null)
                throw new KeyNotFoundException("Answer not found for this question");

            return new QuestionResultDTO
            {
                IsCorrect = answer.IsCorrect,
            };
        }

       
    }
}
