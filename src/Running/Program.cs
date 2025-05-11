using RunningTracker.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersPolicy();
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddCustomServices();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

// only for demonstration purposes, shouldn't be exposed
app.UseDeveloperExceptionPage();
app.UseSwagger();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{
}