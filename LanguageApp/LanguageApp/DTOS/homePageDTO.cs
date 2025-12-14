namespace LanguageApp.DTOS
{
    public class homePageDTO
    {
        public string LanName { get; set; } = string.Empty;
        public string LanFlag { get; set; } = string.Empty;
        public int streakScore { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLesson { get; set; }
        public string userLevel { get; set; } = string.Empty;
        public IEnumerable<string>? Categories { get; set; }

    }
}
