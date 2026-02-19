using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Domain.Entities;
using MediatR;

namespace InternetMarket.CartService.Application.CartItems.Create
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly ICartRepository _cartRepository;

        public AddCartItemCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId);

            if (cart is null)
            {
                cart = new Cart(request.UserId);
                await _cartRepository.CreateAsync(cart);
            }

            cart.AddItem(request.ProductId, request.Quantity);
            await _cartRepository.UpdateAsync(cart);
        }
    }
}