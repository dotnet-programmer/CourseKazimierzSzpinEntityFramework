using MediatR;
using Shop.Application.Common.Exceptions;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Commands;

public class UpdateOrderCommandHandler(IAppDbContext appDbContext) : IRequestHandler<UpdateOrderCommand, Unit>
{
	public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		var order = await appDbContext.Orders.FindAsync(request.Id) ?? throw new NotFoundException(nameof(Order), request.Id);
		order.Title = request.Title;
		order.MethodPayment = request.MethodPayment;
		await appDbContext.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}