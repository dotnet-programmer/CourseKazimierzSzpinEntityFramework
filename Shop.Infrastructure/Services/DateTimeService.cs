using Shop.Application.Common.Interfaces;

namespace Shop.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
	public DateTime Now => DateTime.Now;
}