namespace LanguageApp.DTOS
{
    public class LevelDTORequest
    {
        public int LanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
