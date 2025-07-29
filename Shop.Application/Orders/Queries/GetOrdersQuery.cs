using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}