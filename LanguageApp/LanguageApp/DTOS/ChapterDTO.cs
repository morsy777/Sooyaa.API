namespace LanguageApp.DTOS
{
    public class ChapterDTO
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderNumber { get; set; }

    }
}
