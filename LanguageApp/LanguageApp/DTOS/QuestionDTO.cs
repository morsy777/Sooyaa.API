namespace LanguageApp.DTOS
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? Explanation { get; set; }
    }
}
