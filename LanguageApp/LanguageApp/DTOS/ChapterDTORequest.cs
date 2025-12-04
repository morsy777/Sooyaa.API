namespace LanguageApp.DTOS
{
    public class ChapterDTORequest
    {
        public int LevelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderNumber { get; set; }
    }
}
