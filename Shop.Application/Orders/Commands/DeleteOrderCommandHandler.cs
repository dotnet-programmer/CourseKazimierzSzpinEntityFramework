using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Commands;

public class DeleteOrderCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteOrderCommand, Unit>
{
	public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		appDbContext.Orders.Remove(new Order { Id = request.Id });
		await appDbContext.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}