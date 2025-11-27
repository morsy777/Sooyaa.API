
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Description)
            .HasMaxLength(300);

        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .HasMany(x => x.Chapters)
            .WithOne(x => x.Level)
            .HasForeignKey(x => x.LevelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
