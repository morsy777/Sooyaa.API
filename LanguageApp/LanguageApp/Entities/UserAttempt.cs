namespace LanguageApp.Entities;

public class UserAttempt
{
    public int Id { get; set; }
    public int AttemptsCountLeft { get; set; }
    public DateTime LastAttemptAt { get; set; }

    public string? UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    public int? QuestionId { get; set; }
    public Question Question { get; set; } = default!;
}