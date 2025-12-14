namespace LanguageApp.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int OrderNumber { get; set; }

    public int? ChapterId { get; set; }
    public Chapter Chapter { get; set; } = default!;

    public int? CategoryId { get; set; }
    public Category Category { get; set; } = default!; // Vocab, Grammar or other

    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<UserProgress> UserProgress { get; set; } = new List<UserProgress>();
    public ICollection<SavedLesson> SavedLessons { get; set; }=new List<SavedLesson>();

}