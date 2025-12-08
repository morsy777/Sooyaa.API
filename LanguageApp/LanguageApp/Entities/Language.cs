namespace LanguageApp.Entities;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<UserLanguage> UserLanguages { get; set; } = new();
    public List<Level> Levels { get; set; } = new();
    public ICollection<WordList> WordLists { get; set; } = new List<WordList>();
}
