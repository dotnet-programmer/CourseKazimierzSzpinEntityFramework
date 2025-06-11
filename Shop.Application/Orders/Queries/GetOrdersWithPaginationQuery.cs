using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mappings;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Extensions;

namespace Shop.Application.Orders.Queries;
public class GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
}

public class GetOrdersWithPaginationQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
{
	private readonly IAppDbContext _context = appDbContext;

	public Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
	{
		var orders = _context.Orders
			.AsNoTracking()
			.OrderBy(x => x.Id)
			.Select(x => x.ToDto())
			.PaginatedListAsync(request.PageNumber, request.PageSize);

		return orders;
	}
}
