using System.Security.Claims;
using Shop.Application.Common.Interfaces;

namespace Shop.WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
	//Pobieranie wartości hard-coded
	public string UserId
		=> httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "4131-0102-1312-3123-5121";
}