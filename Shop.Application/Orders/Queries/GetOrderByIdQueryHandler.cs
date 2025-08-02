using MediatR;
using Shop.Application.Common.Exceptions;
using Shop.Application.Common.Interfaces;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Queries;

public class GetOrderByIdQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
	public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
	{
		var order = await appDbContext.Orders.FindAsync(request.Id);

		return order == null
			? throw new NotFoundException(nameof(Order), request.Id)
			: order.ToDto();
	}
}