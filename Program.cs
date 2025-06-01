using YokIstatistikWeb.Models; // <-- mevcut
using YokIstatistikWeb.Services; // <-- mevcut
using YokIstatistikWeb.Models;     // <-- Eðer MongoDbContext bu namespace'teyse ekle

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MongoDB yapýlandýrmasý
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<UniversiteService>();

// ?? MongoDbContext eklenmeli
builder.Services.AddSingleton<MongoDbContext>();

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
