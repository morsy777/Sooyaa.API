using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Org.BouncyCastle.Tls;
using LanguageApp.Settings;
using LanguageApp.Application.Bussiness;
using LanguageApp.Application.IBussiness;
using LanguageApp.Mapping;
using LanguageApp.Dashboard.Interface;
using LanguageApp.Dashboard.Service;
using CloudinaryDotNet;

namespace LanguageApp;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Add CORS Policy
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", builder =>
            {
                builder.WithOrigins(
                        "http://localhost:5173",
                        "https://sooyaa-dashboard.vercel.app"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Database
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString));

        // Controllers
        services.AddControllers();

        // Swagger
        services.AddSwaggerServices();

        // Mapster
        services.AddMapsterConfig();

        // FluentValidation
        services.AddFluentValidationConfig();

        // Auth & Identity
        services.AddAuthConfig(configuration);

        // Hangfire
        services.AddBackgroundJobsConfig(configuration);

        // Scoped Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        // Mail Settings
        services.AddScoped<IEmailSender, EmailService>();
        //services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
        services.AddHttpContextAccessor();

        // -----------------------------
        // Cloudinary Registration
        // -----------------------------
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
        var cloudinary = new Cloudinary(cloudinaryAccount);

        services.AddSingleton(cloudinary); // register Cloudinary
        services.AddScoped<IMediaService, MediaService>(); // register your MediaService

        // Other Services
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IhomeService, homeService>();
        services.AddScoped<IWordListService, WordListService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswersService, AnswerService>();


        // Mapster Global Mapping
        TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingWordList).Assembly);

        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }

    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                };
            });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

        return services;
    }

    private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"))
        );

        // Add the processing server as IHostedService
        services.AddHangfireServer();
        return services;
    }
}
