using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.API.Extensions;
using InternetMarket.CartService.Application.Carts;
using InternetMarket.CartService.Application.Carts.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InternetMarket.CartService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<CartDto>> GetMyCartAsync()
        {
            var userId = User.GetUserId();
            var cart = await _mediator.Send(new GetCartByUserIdQuery(userId));
            return Ok(cart);
        }
    }
}