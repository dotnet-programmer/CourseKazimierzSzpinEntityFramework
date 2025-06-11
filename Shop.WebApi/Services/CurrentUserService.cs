using System.Security.Claims;
using Shop.Application.Common.Interfaces;

namespace Shop.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		=> _httpContextAccessor = httpContextAccessor;

	//Pobieranie wartości hard-coded
	public string UserId
		=> _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
		?? "4131-0102-1312-3123-5121";
}