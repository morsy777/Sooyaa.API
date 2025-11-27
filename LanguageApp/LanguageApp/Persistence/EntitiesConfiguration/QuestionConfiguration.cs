
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.QuestionText)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .Property(x => x.Explanation)
            .HasMaxLength(600);

        builder
            .HasIndex(x => new { x.QuestionText, x.LessonId })
            .IsUnique();

        builder
            .HasOne(x => x.Lesson)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
