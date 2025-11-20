namespace LanguageApp.Entities;

public class UserStreak
{
    public int Id { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;
}