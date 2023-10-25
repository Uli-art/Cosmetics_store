using Microsoft.Extensions.DependencyInjection;
using WEB_153502_Sidorova;
using WEB_153502_Sidorova.Services.CosmeticsService;
using WEB_153502_Sidorova.Services.ShopService;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
}).AddCookie("cookie").AddOpenIdConnect("oidc", options =>
 {
     options.Authority =
    builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
     options.ClientId =
    builder.Configuration["InteractiveServiceSettings:ClientId"];
     options.ClientSecret =
    builder.Configuration["InteractiveServiceSettings:ClientSecret"];
     // Получить Claims пользователя
     options.GetClaimsFromUserInfoEndpoint = true;
     options.ResponseType = "code";
     options.ResponseMode = "query";
     options.SaveTokens = true;
 });

//var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IShopService, MemoryShopService>();
builder.Services.AddScoped<ICosmeticsService, MemoryCosmeticsService>();

UriData uriData = new UriData(config["UriData:ApiUri"], config["UriData:ISUri"]);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient<ICosmeticsService, ApiCosmeticsService>(opt => opt.BaseAddress = new Uri(config["UriData:ApiUri"]));
builder.Services.AddHttpClient<IShopService, ApiShopService>(opt => opt.BaseAddress = new Uri(config["UriData:ApiUri"]));
builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{category?}",
    new { controller = "Home", action = "Index"}
    );

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
