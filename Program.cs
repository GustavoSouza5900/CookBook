using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CookBookDB>(options => options.UseInMemoryDatabase("db"));
builder.Services.AddSession();

var app = builder.Build();

app.UseSession();
app.MapControllerRoute("default", "/{controller=recipe}/{action=list}/{id?}");
app.UseStaticFiles();
app.Run();
