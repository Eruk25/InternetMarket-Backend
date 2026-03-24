using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.API.Extensions;
using InternetMarket.OrderService.Application.Orders;
using InternetMarket.OrderService.Application.Orders.Create;
using InternetMarket.OrderService.Application.Orders.Delete;
using InternetMarket.OrderService.Application.Orders.Get.GetAll;
using InternetMarket.OrderService.Application.Orders.Get.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternetMarket.OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllByIdAsync()
        {
            var userId = User.GetUserId();
            var orders = await _mediator.Send(new GetAllOrdersByUserIdQuery(userId));
            return Ok(orders);
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<ActionResult<OrderDto>> GetByIdAsync([FromRoute] Guid orderId)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(orderId));
            return order;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync()
        {
            var userId = Guid.Parse("184CF4DB-492A-4419-FD61-08DE6D5434FE");
            await _mediator.Send(new CreateOrderCommand(userId));
            return Ok();
        }

        [HttpDelete]
        [Route("cancel/{orderId}")]
        public async Task<IActionResult> CancelAsync([FromRoute] Guid orderId)
        {
            await _mediator.Send(new CancelOrderCommand(orderId));
            return Ok();
        }
    }
}