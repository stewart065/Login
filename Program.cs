using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using login.Entities;
using login.Identity;
using AutoMapper;
using login.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using login.ViewModel;
using login.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework DbContext for your application database
builder.Services.AddDbContext<dataofmedContext>(
    options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)), // Update to your MySQL version
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )
    )
);


builder.Services.AddDbContext<sampleappContext>(options => 
    options.UseMySql("server=sql12.freesqldatabase.com;database=sql12749728;user=sql12749728;password=xaqCDkV8a4;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.62-mariadb")));

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseMySql("server=sql12.freesqldatabase.com;database=sql12749728;user=sql12749728;password=xaqCDkV8a4;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.62-mariadb")));

// Configure Identity with custom settings
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.SignIn.RequireConfirmedAccount = false;
    option.SignIn.RequireConfirmedEmail = false;
    option.SignIn.RequireConfirmedPhoneNumber = false;
    option.User.RequireUniqueEmail = true;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.Password.RequiredUniqueChars = 0;
    option.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationIdentityDbContext>();

// Configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<SampleAppProfile>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);

// Add Authorization and Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// Add CORS policy to allow requests from your Flutter app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:45336", "http://localhost:52252") // Replace with your Flutter app's origins
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable CORS with the specified policy
app.UseCors("AllowFlutterApp");

// Enable Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Custom error handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Log the exception (use a logging framework in production)
        var logger = context.RequestServices.GetService<ILogger<Program>>();
        logger?.LogError(ex, "An unhandled exception occurred.");
        
        // Return a generic error response
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    }
});

// Configure default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
