using MediatR;

namespace Shop.Application.Orders.Commands;

public class DeleteOrderCommand : IRequest<Unit>
{
	public int Id { get; set; }
}