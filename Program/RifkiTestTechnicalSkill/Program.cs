using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RifkiTestTechnicalSkill.Data;
using RifkiTestTechnicalSkill.Services.Interface;
using RifkiTestTechnicalSkill.Services;
using Microsoft.Extensions.DependencyInjection;
using CalculatorService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders(); ;
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHomeService, HomeService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IUserOrderService, UserOrderService>();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IReportService, ReportService>();

builder.Services.AddScoped<CalculatorService.CalculatorSoapClient>(provider =>
{
    return new CalculatorService.CalculatorSoapClient(CalculatorService.CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);
});

var app = builder.Build();
// Uncomment it when you run the project first time, It will registered an admin
//using (var scope = app.Services.CreateScope())
//{
//    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
//}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
