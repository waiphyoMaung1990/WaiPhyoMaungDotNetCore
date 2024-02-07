using Microsoft.EntityFrameworkCore;
using WaiPhyoMaungDontNetCore.MvcApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;
    opt.UseSqlServer(connectionString);
});


//builder.Services.AddDbContext<AppDbContext>(optionsAction: opt=:DbContextOptionsBuilder =>
//{
//    OptionsBuilderConfigurationExtensions.
//}
var app = builder.Build();

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
    pattern: "{controller=BlogAjax}/{action=Index}/{id?}");

app.Run();
