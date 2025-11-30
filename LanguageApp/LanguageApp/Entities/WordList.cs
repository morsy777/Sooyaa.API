namespace LanguageApp.Entities
{
    public class WordList
    {
        public int Id { get; set; }
        public string ArabicWord { get; set; } = null!;
        public string EnglishWord { get; set; } = null!;


        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}
