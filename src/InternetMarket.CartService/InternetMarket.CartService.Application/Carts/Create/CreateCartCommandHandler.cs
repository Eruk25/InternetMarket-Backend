using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Domain.Entities;
using MediatR;

namespace InternetMarket.CartService.Application.Carts.Create
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand>
    {
        private readonly ICartRepository _cartRepository;

        public CreateCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart(request.UserId);
            await _cartRepository.CreateAsync(cart);
        }
    }
}