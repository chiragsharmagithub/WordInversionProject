using Microsoft.EntityFrameworkCore;
using WordInversionProject.Data;
using WordInversionProject.Interfaces;
using WordInversionProject.Repositories;
using WordInversionProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configure Database connection (SQLite)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlite(
		connectionString
	),
	ServiceLifetime.Scoped
);

// Register custom repositories and services (Dependency Injection)
builder.Services.AddScoped<IWordInversionRepository, WordInversionRepository>();
builder.Services.AddScoped<IWordInversionService, WordInversionService>();

// Configure logging
builder.Services.AddLogging(config =>
{
	config.AddConsole();
	config.AddDebug();
	config.SetMinimumLevel(LogLevel.Information);
});

// Build application
var app = builder.Build();

// Initialize Database
try
{
	using (var scope = app.Services.CreateScope())
	{
		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		dbContext.Database.EnsureCreated();
		app.Logger.LogInformation("Database initialized successfully");
	}
}
catch(Exception ex)
{
	app.Logger.LogError(ex, "Error during database initialization");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllers();
app.MapRazorPages();

app.Run();
