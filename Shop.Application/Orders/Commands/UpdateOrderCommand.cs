using MediatR;
using Shop.Domain.Enums;

namespace Shop.Application.Orders.Commands;

public class UpdateOrderCommand : IRequest<Unit>
{
	public int Id { get; set; }
	public string Title { get; set; } = default!;
	public MethodPayment MethodPayment { get; set; }
}