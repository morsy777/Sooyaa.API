namespace LanguageApp.Entities;

public class UserProgress
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = default!;
}