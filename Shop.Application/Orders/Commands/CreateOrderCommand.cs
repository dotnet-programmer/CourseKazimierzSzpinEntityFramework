using MediatR;
using Shop.Domain.Enums;

namespace Shop.Application.Orders.Commands;

public class CreateOrderCommand : IRequest<int>
{
	public string Title { get; set; } = default!;
	public MethodPayment MethodPayment { get; set; }
}