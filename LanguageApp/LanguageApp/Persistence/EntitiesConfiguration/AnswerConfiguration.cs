
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.AnswerText)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.IsCorrect)
            .IsRequired();

        builder
            .HasIndex(x => new { x.AnswerText, x.QuestionId })
            .IsUnique();

        builder
            .HasOne(x => x.Question)
            .WithMany(x => x.Answers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
