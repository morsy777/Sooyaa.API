
namespace LanguageApp.Persistence.EntitiesConfiguration
{
    public class WordListConfiguration : IEntityTypeConfiguration<WordList>
    {
        public void Configure(EntityTypeBuilder<WordList> builder)
        {
            
            builder.ToTable("WordLists");

            builder.HasKey(wl => wl.Id);

            builder.Property(wl => wl.Id)
                .UseIdentityColumn(1,1);

            builder.Property(w => w.ArabicWord)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(w => w.EnglishWord)
                .IsRequired()
                .HasMaxLength(200);


            builder.HasIndex(w => new { w.UserId, w.ArabicWord })
              .IsUnique();

            builder.HasOne(w => w.User)
               .WithMany(u => u.UserWords)
               .HasForeignKey(w => w.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
