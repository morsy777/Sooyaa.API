
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class UserProgressConfiguration : IEntityTypeConfiguration<UserProgress>
{
    public void Configure(EntityTypeBuilder<UserProgress> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.IsCompleted)
            .IsRequired();

        builder
            .HasIndex(x => new { x.UserId, x.LessonId })
            .IsUnique();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserProgress)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Lesson)
            .WithMany(x => x.UserProgress)
            .HasForeignKey(x => x.LessonId);
    }
}
