namespace LanguageApp.Entities;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string? Explanation { get; set; }

    public int? LessonId { get; set; }
    public Lesson Lesson { get; set; } = default!;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}