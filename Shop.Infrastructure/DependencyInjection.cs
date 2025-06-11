using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Common.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Services;

namespace Shop.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IDateTimeService, DateTimeService>();

		// dla każdego wywołania IAppDbContext, będzie tworzony nowy kontekst bazy danych AppDbContext
		services.AddScoped<IAppDbContext, AppDbContext>();
		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		});
		//services.AddDbContext<AppDbContext>(options =>
		//	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
		//	b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

		return services;
	}
}