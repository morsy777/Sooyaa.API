namespace LanguageApp.Persistence.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .OwnsMany(u => u.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(100);

        builder
            .Property(x => x.LastName)
            .HasMaxLength(100);

        builder
            .Property(x => x.IsPremium)
            .HasDefaultValue(false);

    }
}
