using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RunningTracker.Extensions;
using RunningTracker.Utils;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomControllers();

// Add custom extensions
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddCustomServices();

// Add API versioning
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RunningTracker API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();