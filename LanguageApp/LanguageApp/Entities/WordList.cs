namespace LanguageApp.Entities
{
    public class WordList
    {
        public int Id { get; set; }
        public string ArabicWord { get; set; } = null!;
        public string ForeignWord { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; } = default!;
    }
}
