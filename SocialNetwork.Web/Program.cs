using Microsoft.EntityFrameworkCore;
using SocialNetwork.Persistance.Context;
using SocialNetwork.IOC.Dependencies.dbo;
using SocialNetwork.IOC.Dependencies.infraestructure;
using SocialNetwork.Identity.Register;
using SocialNetwork.Web.Middlewares;
using SocialNetwork.Web.Helpers.Perfil;
using SocialNetwork.Web.Helpers.perfil;
using SocialNetwork.Web.Helpers.perfil.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. SocialNetwork
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SocialNetworkContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SocialNetwork")));

// Registro de las dependencias del esquema dbo
builder.Services.AddDboDependency();

// Registro de las dependencias de la capa Infraestructure
builder.Services.AddEmailDependency(builder.Configuration);

// Registro de las dependencias de la capa Identity
builder.Services.AddIdentityLayer(builder.Configuration);

// Registro de las dependencias de los servicios de Identity
builder.Services.AddIdentityService();

// Registro de las dependencias de la web
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddScoped<ValidateUserSesion>();
builder.Services.AddScoped<PerfilHelper>();
//builder.Services.AddScoped<PublicacionesHelper>();
builder.Services.AddScoped<LoadPhoto>();

// Registro para las sesiones
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.RunIdentitySeeds();
}

// Mover app.UseSession() antes de app.UseRouting()
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Home}/{id?}");

await app.RunAsync();
