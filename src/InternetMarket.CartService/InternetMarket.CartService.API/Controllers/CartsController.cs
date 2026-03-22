using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.API.DTOs.Requests;
using InternetMarket.CartService.API.Extensions;
using InternetMarket.CartService.Application.CartItems.Create;
using InternetMarket.CartService.Application.Carts;
using InternetMarket.CartService.Application.Carts.Clear;
using InternetMarket.CartService.Application.Carts.Get;
using InternetMarket.CartService.Infrastructure.Migrations;
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

        [HttpPost]
        [Route("items")]
        public async Task<ActionResult> AddToCartAsync([FromBody] AddCartItemRequest request)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new AddCartItemCommand(userId, request.ProductId, request.Quantity));
            return NoContent();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<CartDto>> GetByIdAsync(Guid userId)
        {
            var cart = await _mediator.Send(new GetCartByUserIdQuery(userId));
            return cart;
        }

        [HttpDelete]
        [Route("clear/{userId}")]
        public async Task<ActionResult> ClearAsync(Guid userId)
        {
            await _mediator.Send(new ClearCartCommand(userId));
            return NoContent();
        }
    }
}