using lucas_2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
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
builder.Services.AddDbContext<AccountContext>
    (o => o.UseSqlServer(connString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<AccountContext>()
      .AddDefaultTokenProviders();
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<MenuList>();
builder.Services.AddScoped<AccountService>();

builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");
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
    pattern: "{controller=Admin}/{action=IndexList}/{id?}"
    ) ;
app.MapControllerRoute(
    name: "addposts",
    pattern: "AddOrEditPosts/{subCategoryName}/{postId?}",
    defaults: new { controller = "Admin", action = "AddOrEditPosts" });
app.MapControllerRoute(
    name: "custom",
    pattern: "{categoryName}/{subCategoryName}/{postId}",
    defaults: new { controller = "Home", action = "PostView" });
app.MapControllerRoute(
    name: "custom2",
    pattern: "{categoryName}",
    defaults: new { controller = "Home", action = "CategoryList" });
app.MapControllerRoute(
    name: "custom3",
    pattern: "{categoryName}/{subCategoryName}",
    defaults: new { controller = "Home", action = "SubCategoryList" });
app.MapControllerRoute(
    name: "register",
    pattern: "Admin/Register",
    defaults: new { controller = "Admin", action = "Register" });
app.MapControllerRoute(
    name: "login",
    pattern: "Admin/Login",
    defaults: new { controller = "Admin", action = "Login" });



app.Run();
