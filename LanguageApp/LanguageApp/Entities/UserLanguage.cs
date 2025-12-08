namespace LanguageApp.Entities;

public class UserLanguage
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    public int LevelId { get; set; }
    public Level Level { get; set; } = default!;

    public int LanguageId { get; set; }
    public Language Language { get; set; } = default!;
}