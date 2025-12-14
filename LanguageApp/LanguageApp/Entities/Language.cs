namespace LanguageApp.Entities;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public ICollection<UserLanguage> UserLanguages { get; set; } = new List<UserLanguage>();
    public ICollection<Level> Levels { get; set; } = new List<Level>();
    public ICollection<WordList> WordLists { get; set; } = new List<WordList>();
    public ICollection<SavedLesson> SavedLessons { get; set; }=new List<SavedLesson>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();


}
