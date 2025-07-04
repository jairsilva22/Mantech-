using AppMantenimiento;
using AppMantenimiento.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);



var politicaUsusariosAutenticados =  new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsusariosAutenticados));
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=DefaultConnection"));


builder.Services.AddAuthentication();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/Account/Login";
    opciones.LogoutPath = "/Account/Login";
    opciones.AccessDeniedPath = "/Account/Login";
});


//Servicios del PatternRepositry
builder.Services.AddScoped<IAccountRepositorio, AccountRepositorio>();
builder.Services.AddScoped<IMaquinasRepositorio, MaquinasRepositorio>();
builder.Services.AddScoped<IQrGeneratorService, QrGeneratorService>();



//Servicio de localizacion  usando IStringLocalizer
builder.Services.AddLocalization(opciones => opciones.ResourcesPath = "Recursos");


var app = builder.Build();

var supportedCultures = Constantes.CulturasUISoportadas.Select(c => new CultureInfo(c.Value)).ToList();
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CookieRequestCultureProvider(), // lee la cultura desde las cookies
        new AcceptLanguageHeaderRequestCultureProvider() // como fallback
    }
};


app.UseRequestLocalization(localizationOptions);

app.UseRequestLocalization(opciones =>
{
    opciones.DefaultRequestCulture = new RequestCulture("es");
    opciones.SupportedUICultures = Constantes.CulturasUISoportadas.Select(cultura => new CultureInfo(cultura.Value)).ToList();
});

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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
