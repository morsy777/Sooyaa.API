namespace LanguageApp.DTOS
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? ChapterId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int OrderNumber { get; set; }
        
    }
}
