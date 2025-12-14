namespace LanguageApp.Persistence.EntitiesConfiguration;

public class UserLanguagesConfiguration : IEntityTypeConfiguration<UserLanguage>
{
    public void Configure(EntityTypeBuilder<UserLanguage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.LanguageId, x.LevelId })
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserLanguages)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);  

        builder.HasOne(x => x.Language)
            .WithMany(x => x.UserLanguages)
            .HasForeignKey(x => x.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);  

        builder.HasOne(x => x.Level)
            .WithMany(x => x.UserLanguages)
            .HasForeignKey(x => x.LevelId)
            .OnDelete(DeleteBehavior.Restrict);  

    }
}
