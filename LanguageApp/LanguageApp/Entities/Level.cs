namespace LanguageApp.Entities;

public class Level
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int? LanguageId { get; set; }
    public Language Language { get; set; } = default!;

    public List<Chapter> Chapters { get; set; } = new();
    public List<ApplicationUser> Users { get; set; } = new(); 
}