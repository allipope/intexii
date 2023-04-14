using intexii.Data;
using intexii.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;


// these lines refer to our connection strings to our databases in postgres that are stored in user-secrets
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:AuthLink"]));

builder.Services.AddDbContext<ebdbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// configure cookie settings
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
});


builder.Services.AddControllersWithViews();


// allows the addition of .NET Identity and enables roles
builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IebdbContextRepository, EFebdbContextRepository>();


// change password requirements to be better
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


// tells our app where the model is
builder.Services.AddCors();
builder.Services.AddSingleton(
      new InferenceSession(Path.Combine(env.ContentRootPath, "wwwroot", "model.onnx")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// enables hsts above, and redirects to https below
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// csp header configuration
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
