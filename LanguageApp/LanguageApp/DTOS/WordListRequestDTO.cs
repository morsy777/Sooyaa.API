namespace LanguageApp.DTOS
{
    public class WordListRequestDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string ArabicWord { get; set; } = string.Empty;
        public string EnglishWord { get; set; } = string.Empty;
    }
}
