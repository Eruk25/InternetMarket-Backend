using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Clients;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Domain.Entities;
using MediatR;

namespace InternetMarket.CartService.Application.CartItems.Create
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductServiceClient _productServiceClient;

        public AddCartItemCommandHandler(ICartRepository cartRepository, IProductServiceClient productServiceClient)
        {
            _cartRepository = cartRepository;
            _productServiceClient = productServiceClient;
        }

        public async Task Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var product = await _productServiceClient.GetProductByIdAsync(request.ProductId);

            if (product is null)
                throw new Exception("Product was not found");

            var cart = await _cartRepository.GetByUserIdAsync(request.UserId);

            if (cart is null)
            {
                cart = new Cart(request.UserId);
                await _cartRepository.CreateAsync(cart);
            }

            cart.AddItem(product.Id, product.ProductName, product.Price, request.Quantity);
            await _cartRepository.UpdateAsync(cart);
        }
    }
}