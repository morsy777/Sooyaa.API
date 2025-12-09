namespace LanguageApp.Entities;

public class Chapter
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int OrderNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int LevelId { get; set; }
    public Level Level { get; set; } = default!;

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}