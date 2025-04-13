using AgenciaEnvios.LogicaAccesoDatos;
using AgenciaEnvios.LogicaAplicacion;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


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



//DI - CASOS USO


builder.Services.AddScoped<ICULogin, CULogin>();
builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
builder.Services.AddScoped<ICUListarUsuarios, CUListarUsuarios>();
builder.Services.AddScoped<ICUEliminarUsuario, CUEliminarUsuario>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
