using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries;

// IRequest<OrderDto> - tzw. marker, oznaczajacy że jest to zapytanie, które zwraca OrderDto
public class GetOrderByIdQuery : IRequest<OrderDto>
{
	// parametr, który będzie przekazywany do zapytania, czyli Id zamówienia
	public int Id { get; set; }
}