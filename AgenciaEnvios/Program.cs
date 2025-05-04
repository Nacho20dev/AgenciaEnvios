using AgenciaEnvios.LogicaAplicacion;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaAccesoDatos.Migrations;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUAgencia;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUAgencia;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AgenciaEnvios");//DefaultConnection debe coincidir con el nombre designado en el JSON.

builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("AgenciaEnvios.LogicaAccesoDatos")
    )
);



// Add services to the container.
builder.Services.AddControllersWithViews();

//DI - REPOS

builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();
builder.Services.AddScoped<IRepositorioEnvio, RepositorioEnvio>();
builder.Services.AddScoped<IRepositorioAgencia, RepositorioAgencia>();



//DI - CASOS USO


builder.Services.AddScoped<ICULogin, CULogin>();
builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
builder.Services.AddScoped<ICUListarUsuarios, CUListarUsuarios>();
builder.Services.AddScoped<ICUEliminarUsuario, CUEliminarUsuario>();
builder.Services.AddScoped<ICUEditarUsuario, CUEditarUsuario>();
builder.Services.AddScoped<ICUObtenerUsuario, CUObtenerUsuario>();
builder.Services.AddScoped<ICUAltaEnvio, CUAltaEnvio>();
builder.Services.AddScoped<ICUObtenerEnvio, CUObtenerEnvio>();
builder.Services.AddScoped<ICUListarEnvios, CUListarEnvios>();
builder.Services.AddScoped<ICUObtenerAgencia, CUObtenerAgencia>();






builder.Services.AddSession();
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


app.UseSession();





app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
