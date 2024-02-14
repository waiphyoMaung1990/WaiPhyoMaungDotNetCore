using Microsoft.EntityFrameworkCore;
using WaiPhyoMaungDontNetCore.MvcApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region for DB
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;
    opt.UseSqlServer(connectionString);
});
#endregion
#region for HttpClient
//builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped(IServiceProvider =>
{
    HttpClient httpClient = new HttpClient()
    {
        BaseAddress = new Uri(builder.Configuration.GetSection(key: "ApiUrl").Value!)
    };
    return httpClient;
});
#endregion

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
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
