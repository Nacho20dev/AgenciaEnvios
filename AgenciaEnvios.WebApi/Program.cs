using AgenciaEnvios.LogicaAccesoDatos;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUAgencia;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUAgencia;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AgenciaEnvios");//DefaultConnection debe coincidir con el nombre designado en el JSON.

builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseSqlServer(connectionString)
);
// Add services to the container.

builder.Services.AddControllers();

//DI - REPOS

builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();
builder.Services.AddScoped<IRepositorioEnvio, RepositorioEnvio>();
builder.Services.AddScoped<IRepositorioAgencia, RepositorioAgencia>();
builder.Services.AddScoped<IRepositorioSeguimiento, RepositorioSeguimiento>();




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
builder.Services.AddScoped<ICUObtenerObjetoEnvio, CUObtenerObjetoEnvio>();
builder.Services.AddScoped<ICUFinalizarEnvio, CUFinalizarEnvio>();
builder.Services.AddScoped<ICUAgregarSeguimiento, CUAgregarSeguimiento>();
builder.Services.AddScoped<ICUObtenerEnvioPorTracking, CUObtenerEnvioPorTracking>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
