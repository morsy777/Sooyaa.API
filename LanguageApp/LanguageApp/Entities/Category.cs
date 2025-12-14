namespace LanguageApp.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();


    public int LanguageId { get; set; }    
    public Language? Language { get; set; }
}
