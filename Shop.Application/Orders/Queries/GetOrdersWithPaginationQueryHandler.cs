using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mappings;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;

namespace Shop.Application.Orders.Queries;

public class GetOrdersWithPaginationQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
{
	public Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
		=> appDbContext.Orders
			.AsNoTracking()
			.OrderBy(x => x.Id)
			.Select(x => x.ToDto())
			.PaginatedListAsync(request.PageNumber, request.PageSize);
}