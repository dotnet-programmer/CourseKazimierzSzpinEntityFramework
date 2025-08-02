using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries;

// brak właściwości w zapytaniu, ponieważ chcemy pobrać wszystkie zamówienia
// zwracany typ to IEnumerable<OrderDto>, czyli kolekcja DTO zamówień
public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}