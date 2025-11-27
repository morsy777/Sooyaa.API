namespace LanguageApp.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }

    public int? ChapterId { get; set; }
    public Chapter Chapter { get; set; } = default!;

    public int? CategoryId { get; set; }
    public Category Category { get; set; } = default!; // Vocab, Grammar or other

    public List<Question> Questions { get; set; } = new();
    public List<UserProgress> UserProgress { get; set; } = new();
}