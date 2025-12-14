namespace LanguageApp.DTOS
{
    public class SaveLessonRequestDTO
    {
        public string userId { get; set; } = string.Empty;
        public int LanId { get; set; }
        public int lessonId { get; set; }

    }
}
