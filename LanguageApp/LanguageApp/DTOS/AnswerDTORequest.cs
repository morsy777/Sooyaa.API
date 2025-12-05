namespace LanguageApp.DTOS
{
    public class AnswerDTORequest
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
