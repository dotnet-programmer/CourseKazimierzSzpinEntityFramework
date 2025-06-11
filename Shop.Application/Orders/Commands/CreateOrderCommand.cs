using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;
using Shop.Domain.Enums;

namespace Shop.Application.Orders.Commands;

public class CreateOrderCommand : IRequest<int>
{
	public string Title { get; set; }
	public MethodPayment MethodPayment { get; set; }
}

public class CreateOrderCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateOrderCommand, int>
{
	private readonly IAppDbContext _context = appDbContext;

	public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		Order order = new()
		{
			Title = request.Title,
			MethodPayment = request.MethodPayment
		};

		_context.Orders.Add(order);
		await _context.SaveChangesAsync(cancellationToken);
		return order.Id;
	}
}