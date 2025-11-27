
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class UserAttemptsConfiguration : IEntityTypeConfiguration<UserAttempt>
{
    public void Configure(EntityTypeBuilder<UserAttempt> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.AttemptsCountLeft)
            .IsRequired();

        builder
            .HasIndex(x => new { x.UserId, x.QuestionId })
            .IsUnique();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserAttempts)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Question)
            .WithMany()
            .HasForeignKey(x => x.QuestionId);
    }
}
