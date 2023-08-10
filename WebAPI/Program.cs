using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var Configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
			.Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CrudDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")
	, ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")),
	options => options.EnableRetryOnFailure(
maxRetryCount: 5,
maxRetryDelay: System.TimeSpan.FromSeconds(30),
errorNumbersToAdd: null)
));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
