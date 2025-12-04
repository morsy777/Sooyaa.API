
namespace LanguageApp.Persistence.EntitiesConfiguration;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(x => x.Content)
            .IsRequired();

        builder
            .Property(x => x.OrderNumber)
            .IsRequired();

        builder
         .HasIndex(x => new { x.Title, x.ChapterId })
         .IsUnique();

        //builder
        //    .HasOne(x => x.Chapter)
        //    .WithMany(x => x.Lessons)
        //    .HasForeignKey(x => x.ChapterId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder
        //    .HasOne(x => x.Category)
        //    .WithMany(x => x.Lessons)
        //    .HasForeignKey(x => x.CategoryId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
