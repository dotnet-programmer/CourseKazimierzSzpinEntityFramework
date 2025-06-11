using Shop.Application.Orders.Dtos;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Extensions;

public static class OrderExtensions
{
	public static OrderDto ToDto(this Order order)
		=> order != null ?
		new OrderDto
		{
			Id = order.Id,
			Title = order.Title,
			MethodPayment = order.MethodPayment
		}
		: throw new ArgumentNullException(nameof(order));

	public static IEnumerable<OrderDto> ToDtos(this IEnumerable<Order> orders)
	{
		if (orders == null || !orders.Any())
		{
			return Enumerable.Empty<OrderDto>();
		}

		return orders.Select(order => order.ToDto());
	}
}