using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<CookBookDB>(options => options.UseInMemoryDatabase("db"));
var constring = "Server=DESKTOP-VIDE7VG\\SQLEXPRESS;Database=CookBook;Trusted_Connection=True;TrustServerCertificate=True";
builder.Services.AddDbContext<CookBookDB>(options => options.UseSqlServer(constring));

builder.Services.AddSession();

var app = builder.Build();

app.UseSession();
app.MapControllerRoute("default", "/{controller=recipe}/{action=list}/{id?}");
app.UseStaticFiles();
app.Run();
