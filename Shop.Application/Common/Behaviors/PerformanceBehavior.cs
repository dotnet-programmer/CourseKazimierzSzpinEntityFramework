using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Common.Interfaces;

namespace Shop.Application.Common.Behaviors;

// badanie wydajności aplikacji
public class PerformanceBehavior<TRequest, TResponse>(ICurrentUserService currentUserService, ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	private readonly Stopwatch _timer = new();
	private readonly ICurrentUserService _currentUserService = currentUserService;
	private readonly ILogger<TRequest> _logger = logger;

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		_timer.Start();
		var response = await next(cancellationToken);
		_timer.Stop();
		var elapsedMilliseconds = _timer.ElapsedMilliseconds;
		if (elapsedMilliseconds > 500)
		{
			var requestName = typeof(TRequest).Name;
			var userId = _currentUserService.UserId ?? string.Empty;
			_logger.LogWarning("Long Request {requestName} ({elapsedMilliseconds} MS) {userId} {request}.", requestName, elapsedMilliseconds, userId, request);
		}
		return response;
	}
}