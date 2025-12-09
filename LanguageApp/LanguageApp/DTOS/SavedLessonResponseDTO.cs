namespace LanguageApp.DTOS
{
    public class SavedLessonResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ChapterName { get; set; } = string.Empty;
        public string LevelName { get; set; } = string.Empty;
        public string LanguageName { get; set; } = string.Empty;
    }
}
