using FinanciarTeApi.DataContext;
using FinanciarTeApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServiceCiudades, ServiceCiudades>();
builder.Services.AddScoped<IServiceProvincia, ServiceProvincia>();
builder.Services.AddScoped<IServiceCliente, ServiceCliente>();
builder.Services.AddScoped<IServicePrestamo, ServicePrestamo>();
builder.Services.AddScoped<IServiceCuotas, ServiceCuotas>();
builder.Services.AddScoped<IServiceDetalleTransacciones, ServiceDetalleTransacciones>();
builder.Services.AddScoped<IServiceTransacciones, ServiceTransacciones>();
builder.Services.AddScoped<IServiceCategoria, ServiceCategoria>();
builder.Services.AddScoped<IServiceEntidadesFinancieras, ServiceEntidadesFinancieras>();
builder.Services.AddScoped<IServiceTiposEntidadFinanciera, ServiceTiposEntidadFinanciera>();
builder.Services.AddScoped<IServiceTiposUsuarios, ServiceTiposUsuarios>();
builder.Services.AddScoped<IServiceTipoTransaccion, ServiceTipoTransaccion>();
builder.Services.AddScoped<IServiceDolar, ServiceDolar>();
builder.Services.AddScoped<IServiceReportes, ServiceReportes>();
builder.Services.AddScoped<IServiceRegistro, ServiceRegistro>();
builder.Services.AddScoped<IServiceSecurity, ServiceSecurity>();
builder.Services.AddScoped<IServiceUsuario, ServiceUsuario>();
builder.Services.AddScoped<IServiceLogin, ServiceLogin>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FinanciarTeContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
