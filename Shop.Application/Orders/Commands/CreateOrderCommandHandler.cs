using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Commands;

public class CreateOrderCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateOrderCommand, int>
{
	public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		Order order = new()
		{
			Title = request.Title,
			MethodPayment = request.MethodPayment
		};

		appDbContext.Orders.Add(order);
		await appDbContext.SaveChangesAsync(cancellationToken);
		return order.Id;
	}
}