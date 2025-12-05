namespace LanguageApp.DTOS
{
    public class LessonDTORequest
    {
        public int ChapterId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int OrderNumber { get; set; }

    }
}
