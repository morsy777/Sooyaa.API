var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// ============================
//    MIDDLEWARE PIPELINE
// ============================

// Swagger (can be first)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS Redirect
app.UseHttpsRedirection();

// ------------------------------------------
// CORS MUST COME BEFORE AUTH & CONTROLLERS
// ------------------------------------------
app.UseCors("AllowFrontend");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Hangfire Dashboard
app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
        }
    ],
    DashboardTitle = "Survery Basket Dashboard",
});

// Map Controllers
app.MapControllers();

app.UseStaticFiles();

app.Run();
