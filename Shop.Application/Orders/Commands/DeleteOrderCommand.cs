using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Commands;

public class DeleteOrderCommand : IRequest
{
	public int Id { get; set; }
}

public class DeleteOrderCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteOrderCommand>
{
	private readonly IAppDbContext _context = appDbContext;

	public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		_context.Orders.Remove(new Order { Id = request.Id });
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}