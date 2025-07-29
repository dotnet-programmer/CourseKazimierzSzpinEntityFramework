﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Commands;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Queries;

namespace Shop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
	private ISender? _mediator;

	public ISender Mediator
		=> _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;

	[HttpGet("GetOrderById")]
	public async Task<ActionResult<OrderDto>> GetOrderById([FromQuery] GetOrderByIdQuery query)
		=> await Mediator.Send(query);

	[HttpGet("GetOrders")]
	public async Task<IEnumerable<OrderDto>> GetOrders([FromQuery] GetOrdersQuery query)
		=> await Mediator.Send(query);

	[HttpGet("GetOrdersWithPagination")]
	public async Task<ActionResult<PaginatedList<OrderDto>>> GetOrdersWithPagination([FromQuery] GetOrdersWithPaginationQuery query)
		=> await Mediator.Send(query);

	[HttpPost]
	public async Task<ActionResult<int>> CreateOrder([FromQuery] CreateOrderCommand command)
		=> await Mediator.Send(command);

	[HttpDelete]
	public async Task<ActionResult> Delete(int id)
	{
		await Mediator.Send(new DeleteOrderCommand { Id = id });
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> Update(UpdateOrderCommand command)
	{
		await Mediator.Send(command);
		return NoContent();
	}
}