using Api.DAO;
using Api.Handlers;
using Api.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new DbConfig(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API PRUEBA", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
    );
});

builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<ClienteHandler>();

builder.Services.AddScoped<DetalleOrdenDAO>();
builder.Services.AddScoped<DetalleOrdenHandler>();

builder.Services.AddScoped<CategoriaDAO>();
builder.Services.AddScoped<CategoriaHandler>();

builder.Services.AddScoped<ProductoDAO>();
builder.Services.AddScoped<ProductoHandler>();

builder.Services.AddScoped<OrdenDAO>();
builder.Services.AddScoped<OrdenHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
