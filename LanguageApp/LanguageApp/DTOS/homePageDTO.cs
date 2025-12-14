namespace LanguageApp.DTOS
{
    public class homePageDTO
    {
        public string LanName { get; set; } = string.Empty;
        public int streakScore { get; set; }
        public string userLevel { get; set; } = string.Empty;
        public IEnumerable<string>? Categories { get; set; }
    }
}
