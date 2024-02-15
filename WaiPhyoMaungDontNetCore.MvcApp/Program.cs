using Microsoft.EntityFrameworkCore;
using Refit;
using RestSharp;
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

#region for RestClient
builder.Services.AddScoped(IServiceProvider =>
{
    RestClient restClient = new RestClient(builder.Configuration.GetSection(key: "ApiUrl").Value!);   
    return restClient;
});
#endregion

#region refit
builder.Services
    .AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!));

var app = builder.Build();
#endregion

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
