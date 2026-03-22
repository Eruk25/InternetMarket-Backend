using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.CartService.Application.Carts.Clear
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand>
    {
        private readonly ICartRepository _cartRepository;

        public ClearCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId);

            if (cart is null)
                throw new Exception("Cart is empty");

            cart.Clear();
            await _cartRepository.UpdateAsync(cart);
        }
    }
}