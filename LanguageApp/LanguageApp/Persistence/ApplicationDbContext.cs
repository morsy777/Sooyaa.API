namespace LanguageApp.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : 
    IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<UserLanguage> UserLanguages { get; set; }
    public DbSet<UserProgress> UserProgresses { get; set; }
    public DbSet<UserStreak> UserStreaks { get; set; }
    public DbSet<WordList> WordLists { get; set; }
    public DbSet<SavedLesson> savedLessons { get; set; }


    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        return base.SaveChangesAsync(cancellationToken);
    }


}
