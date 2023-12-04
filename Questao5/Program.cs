using MediatR;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Questao5.Infrastructure.Configuration;
using Questao5.Infrastructure.Configuration.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDependencyInjection(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

SwaggerConfiguration? swaggerSection = builder.Configuration.GetSection("Swagger").Get<SwaggerConfiguration>();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc(swaggerSection?.Version ?? "v1", new OpenApiInfo()
    {
        Title = swaggerSection?.DocumentTitle ?? "API",
        Description = swaggerSection?.Description ?? "",
        Version = swaggerSection?.Version ?? "v1",
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


