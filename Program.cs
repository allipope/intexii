using intexii.Data;
using intexii.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

//var connectionStringAuth = builder.Configuration.GetConnectionString("AuthLink");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionStringAuth));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var authConnectString = builder.Configuration["ConnectionStrings:AuthLink"];
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(authConnectString));

builder.Services.AddDbContext<ebdbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ebdbContext>(options =>
//    options.UseNpgsql(connectionString));

//var AuthconnectionString = builder.Configuration.GetConnectionString("AuthConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(AuthconnectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddControllersWithViews();

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IebdbContextRepository, EFebdbContextRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz_@.1234567890";
});

//rename denied and authenticated routes
// builder.Services.Configure<CookieAuthenticationOptions>(
//        IdentityConstants.ApplicationScheme,
//     options => {
//         options.LoginPath = "/Authenticate";
//         options.AccessDeniedPath = "/NotAllowed";
//     });


//require users to be authenticated
// builder.Services.AddAuthorization(options =>
// {
//     options.FallbackPolicy = new AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .Build();
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("X-Xss-Protection", "1");
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        context.Response.Headers.Add("Feature-Policy", 
        "vibrate 'self' ; " +
        "camera 'self' ; " +
        "microphone 'self' ; " +
        "speaker 'self' https://youtube.com https://www.youtube.com ;" +
        "geolocation 'self' ; " +
        "gyroscope 'self' ; " +
        "magnetometer 'self' ; " +
        "midi 'self' ; " +
        "sync-xhr 'self' ; " +
        "push 'self' ; " +
        "notifications 'self' ; " +
        "fullscreen '*' ; " +
        "payment 'self' ; " );

        context.Response.Headers.Add(  
        "Content-Security-Policy-Report-Only",  
        "default-src 'self'; " +
        "script-src-elem 'self'" +  
        "style-src-elem 'self'; " +  
        "img-src 'self'; http://www.w3.org/" +
        "font-src 'self'" +
        "media-src 'self'" +
        "frame-src 'self'" +
        "connect-src "

        );  
        await next();  
    });

app.Run();
