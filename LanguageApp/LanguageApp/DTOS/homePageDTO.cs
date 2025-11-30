namespace LanguageApp.DTOS
{
    public class homePageDTO
    {
        public int streakScore { get; set; }
        public string userLevel { get; set; } = string.Empty;
        public IEnumerable<string>? Categories { get; set; }
    }
}
