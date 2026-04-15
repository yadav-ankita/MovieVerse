using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieVerse.Configurations;
using MovieVerse.Models;
using MovieVerse.Models.GenericRepo;
using MovieVerse.Models.Repo;
using System.Net.Http.Headers;
DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetupAppSettings();

var connectionString = AppSettings.ConnectionStrings.DefaultConnection;
var adminDetails = AppSettings.AdminDetails;

builder.Services.AddHttpClient("TMDBClient", client => {
    var config = builder.Configuration.GetSection("TMDB");
    client.BaseAddress = new Uri(config["BaseUrl"]);
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//this do the email confirmation
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = false);
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IAPICalls, APICalls>();
builder.Services.AddScoped<IRecordsRepo, RecordsRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
});
var app = builder.Build();
using var scope = app.Services.CreateScope();

using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await appDbContext.Database.MigrateAsync();


using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!await roleManager.RoleExistsAsync("Admin"))
{
    var adminRole = new IdentityRole("Admin");
    await roleManager.CreateAsync(adminRole);
}

if (!await roleManager.RoleExistsAsync("User"))
{
    var userRole = new IdentityRole("User");
    await roleManager.CreateAsync(userRole);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
      
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LandingPage}/{action=LandingPage}/{id?}")
    .WithStaticAssets();
//app.MapRazorPages();
app.Run();
