
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class UserLanguagesConfiguration : IEntityTypeConfiguration<UserLanguage>
{
    public void Configure(EntityTypeBuilder<UserLanguage> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => new { x.UserId, x.LanguageId })
            .IsUnique();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserLanguages)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Language)
            .WithMany(x => x.UserLanguages)
            .HasForeignKey(x => x.LanguageId);
    }
}
