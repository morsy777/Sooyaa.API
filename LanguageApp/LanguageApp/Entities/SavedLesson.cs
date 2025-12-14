namespace LanguageApp.Entities
{
    public class SavedLesson
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int LessonId { get; set; }
        public int LanguageId { get; set; }

        public ApplicationUser? User { get; set; }
        public Lesson? Lesson { get; set; }
        public Language? Language { get; set; }
    }
}
