namespace LanguageApp.Application.IBussiness
{
    public interface IAnswersService
    {
        Task<QuestionResultDTO> CheckAnswerAsync(int questionId,int selectedOptionId,CancellationToken cancellationToken);
    }
}
