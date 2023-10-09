using Microsoft.Extensions.DependencyInjection;
using WEB_153502_Sidorova;
using WEB_153502_Sidorova.Services.CosmeticsService;
using WEB_153502_Sidorova.Services.ShopService;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();
//var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IShopService, MemoryShopService>();
builder.Services.AddScoped<ICosmeticsService, MemoryCosmeticsService>();

UriData uriData = new UriData(config["UriData:ApiUri"], config["UriData:ISUri"]);

builder.Services.AddHttpClient<ICosmeticsService, ApiCosmeticsService>(opt => opt.BaseAddress = new Uri(config["UriData:ApiUri"]));
builder.Services.AddHttpClient<IShopService, ApiShopService>(opt => opt.BaseAddress = new Uri(config["UriData:ApiUri"]));


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
    pattern: "{controller=Home}/{category?}",
    new { controller = "Home", action = "Index" }
    );


app.Run();
