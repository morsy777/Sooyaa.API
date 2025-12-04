namespace LanguageApp.DTOS
{
    public class QuestionDTORequest
    {
        public int LessonId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? Explanation { get; set; }
    }
}
