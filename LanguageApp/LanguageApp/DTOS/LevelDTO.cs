namespace LanguageApp.DTOS
{
    public class LevelDTO
    {
        public int Id { get; set; }
        public int LanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
