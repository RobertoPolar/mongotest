using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using mongotest.Models;
using mongotest.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

string[] corsPermitedDomains = new[]
    {
        "http://localhost:8080"
    };

builder.Services.AddCors
(
    options =>
    {
        options.AddPolicy
        (
            MyAllowSpecificOrigins,
            builder =>
            {
                builder
                    .WithOrigins(corsPermitedDomains)
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }
        );
    }
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conexion = new ConexionSettings
{
    Server = Environment.GetEnvironmentVariable("DB_SERVER"),
    Database = Environment.GetEnvironmentVariable("DB_DATABASE"),
    Colection = Environment.GetEnvironmentVariable("DB_COLECTION")
};

builder.Services.Configure<ConexionSettings>(builder =>
{
    builder.Server = conexion.Server;
    builder.Database = conexion.Database;
    builder.Colection = conexion.Colection;
});

builder.Services.AddSingleton<GuerreroService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
