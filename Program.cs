using Microsoft.EntityFrameworkCore;
using System;
using VivaFestAPI.Data;
using VivaFestAPI.Extensions;
using VivaFestAPI.Utility;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Ensure config.json exists
var configPath = Path.Combine(builder.Environment.ContentRootPath, "config.json");
if (!File.Exists(configPath))
{
    File.WriteAllText(configPath, "{\n  \"active\": true\n}");
}

var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAllowedSpecificOrigins();
// Add Quiz Module isolated slice
builder.Services.AddQuizModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
