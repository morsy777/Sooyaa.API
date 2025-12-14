
namespace LanguageApp.Persistence.EntitiesConfiguration
{
    public class savedLessonConfiguration : IEntityTypeConfiguration<SavedLesson>
    {
        public void Configure(EntityTypeBuilder<SavedLesson> builder)
        {
            builder.ToTable("SavedLessons");

            builder.HasKey(sl => sl.Id);


            // User relationship
            builder.HasOne(sl => sl.User)
                   .WithMany(u => u.SavedLessons)
                   .HasForeignKey(sl => sl.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Lesson relationship
            builder.HasOne(sl => sl.Lesson)
                   .WithMany(l => l.SavedLessons)
                   .HasForeignKey(sl => sl.LessonId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Language relationship
            builder.HasOne(sl => sl.Language)
                   .WithMany(l => l.SavedLessons)
                   .HasForeignKey(sl => sl.LanguageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(sl => new { sl.UserId, sl.LessonId, sl.LanguageId })
                   .IsUnique();


        }
    }
}
