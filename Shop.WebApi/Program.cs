using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Shop.Application;
using Shop.Application.Common.Interfaces;
using Shop.Infrastructure;
using Shop.WebApi.Services;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
	logger.Debug("init main");

	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.

	builder.Services.AddControllers();
	// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
	builder.Services.AddOpenApi();

	builder.Services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop.WebApi", Version = "v1" });
	});

	builder.Services.AddHttpContextAccessor();
	builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

	// dodanie NLog 
	builder.Logging.ClearProviders();
	builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
	builder.Host.UseNLog();

	// dodanie Dependency Injection z innych projektów
	builder.Services.AddApplication();
	builder.Services.AddInfrastructure(builder.Configuration);

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.WebApi v1"));

		app.MapOpenApi();
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
	logger.Error(ex, "Stopped program because of exception");
	throw;
}
finally
{
	LogManager.Shutdown();
}