namespace LanguageApp.DTOS
{
    public class WordListRequestDTO
    {
        public string UserId { get; set; } = string.Empty;
        public int LanguageId { get; set; }      
        public string ArabicWord { get; set; } = string.Empty;
        public string ForeignWord { get; set; } = string.Empty;
    }
}
