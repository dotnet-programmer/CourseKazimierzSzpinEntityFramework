using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;

namespace Shop.Application.Orders.Queries;

public class GetOrdersQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
	public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
	{
		var orders = await appDbContext.Orders
			.AsNoTracking()
			.ToListAsync(cancellationToken);

		return orders.ToDtos();
	}
}