using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Common.Behaviors;

namespace Shop.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

		return services;
	}
}