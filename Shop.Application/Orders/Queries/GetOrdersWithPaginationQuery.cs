using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries;

public class GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
}