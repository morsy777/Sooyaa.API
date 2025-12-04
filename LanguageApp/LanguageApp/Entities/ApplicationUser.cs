namespace LanguageApp.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? profileImage { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public int? SelectedLevelId { get; set; }
    public Level? SelectedLevel { get; set; }

    public bool IsPremium { get; set; }
    public DateTime? PremiumStartDate { get; set; }
    public DateTime? PremiumEndDate { get; set; }

    // Relationships
    public ICollection<UserLanguage> UserLanguages { get; set; } = new List<UserLanguage>();
    public ICollection<UserProgress> UserProgress { get; set; } = new List<UserProgress>();
    public UserStreak? UserStreak { get; set; } 
    public ICollection<UserAttempt> UserAttempts { get; set; } = new List<UserAttempt>();
    public ICollection<WordList> UserWords { get; set; } = new List<WordList>();
}
