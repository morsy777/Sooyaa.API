
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Description)
            .HasMaxLength(300);

        builder
            .Property(x => x.OrderNumber)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder
            .HasIndex(x => new { x.Name, x.LevelId })
            .IsUnique();

        builder
            .HasOne(x => x.Level)
            .WithMany(x => x.Chapters)
            .HasForeignKey(x => x.LevelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
