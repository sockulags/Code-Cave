using lucas_2._0.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Hämta connection-strängen från AppSettings.json
var connString = builder.Configuration
    .GetConnectionString("DefaultConnection");

// Registrera Context-klassen för dependency injection
builder.Services.AddDbContext<ApplicationContext>
    (o => o.UseSqlServer(connString));
builder.Services.AddScoped<DataService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
