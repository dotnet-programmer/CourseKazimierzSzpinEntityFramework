using MediatR;
using Shop.Application.Common.Exceptions;
using Shop.Application.Common.Interfaces;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Queries;

// IRequest<OrderDto> - tzw. marker, oznaczajacy że jest to zapytanie, które zwraca OrderDto
public class GetOrderByIdQuery : IRequest<OrderDto>
{
	// parametr, który będzie przekazywany do zapytania, czyli Id zamówienia
	public int Id { get; set; }
}

public class GetOrderByIdQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
	private readonly IAppDbContext _context = appDbContext;

	public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
	{
		var order = await _context.Orders.FindAsync(request.Id);

		if (order == null)
		{
			throw new NotFoundException(nameof(Order), request.Id);
		}

		return order.ToDto();
	}
}