using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;

namespace Shop.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}

public class GetOrdersQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
	private readonly IAppDbContext _context = appDbContext;

	public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
	{
		var orders = await _context.Orders
			.AsNoTracking()
			.ToListAsync();

		return orders.ToDtos();
	}
}