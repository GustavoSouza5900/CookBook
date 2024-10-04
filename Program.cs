var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// builder.Services.AddDbContext<BookDatabase>(options => options.UseInMemoryDatabase("db"));

var app = builder.Build();

app.Run();
