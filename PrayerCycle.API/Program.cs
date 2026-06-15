using System.Reflection;
using Microsoft.OpenApi.Models;
using PrayerCycle.API.Endpoints;
using PrayerCycle.API.Middleware;
using PrayerCycle.Application;
using PrayerCycle.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PrayerCycle API",
        Version = "v1",
        Description = "Aile ibadet takip uygulaması için REST API. Kullanıcı, aile, aile üyesi ve namaz kaydı işlemlerini içerir."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PrayerCycle API v1");
        options.DocumentTitle = "PrayerCycle API";
    });
}

app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();
