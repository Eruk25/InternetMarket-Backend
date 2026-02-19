using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Application.CartItems;
using InternetMarket.CartService.Domain.Entities;
using MediatR;

namespace InternetMarket.CartService.Application.Carts.Get
{
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, CartDto>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartByUserIdQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId);

            if (cart is null)
            {
                cart = new Cart(request.UserId);
                await _cartRepository.CreateAsync(cart);
            }

            var cartItems = cart.Items
                .Select(ci => new CartItemDto(
                    ci.ProductId,
                    ci.Quantity,
                    ci.UntilPrice))
                .ToList();
            return new CartDto(cartItems);
        }
    }
}