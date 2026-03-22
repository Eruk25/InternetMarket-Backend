using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.API.Extensions;
using InternetMarket.OrderService.Application.Orders.Create;
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

        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CreateAsync()
        {
            var userId = Guid.Parse("184CF4DB-492A-4419-FD61-08DE6D5434FE");
            await _mediator.Send(new CreateOrderCommand(userId));
            return Ok();
        }

    }
}