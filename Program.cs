using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// Add DbContext and configure SQL Lite connection
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlite("Data Source=ContosoUniversity.db;Cache=Shared;Pooling=false")
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()); // You can comment this out if you don't need detailed logging in production

// Add session handling
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
});

// Authentication (commented out because if you're not using cookies for authentication, you can remove it)
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.LoginPath = "/Login"; // Defines login path
        options.AccessDeniedPath = "/AccessDenied"; // Defines access denied path
    });

// Build the app
var app = builder.Build();

// Apply migrations and seed the database (you can comment this out if you don't need automatic migrations/seeding)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SchoolContext>();

    context.Database.Migrate(); // Comment this if you do not want auto migration
    DbInitializer.Initialize(context); // Comment this if you do not want auto seeding
}

// Configure error handling for production environment
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Comment this out if you don't need HTTP Strict Transport Security (HSTS) in production
}
else
{
    app.UseDeveloperExceptionPage(); // You can comment this out for production
}

// Use session middleware (you need this for session management)
app.UseSession();

// Use authentication middleware (if you plan to use authentication with cookies, uncomment the authentication middleware line)
// app.UseAuthentication(); 

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();
app.Run();
