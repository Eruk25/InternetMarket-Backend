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
                throw new Exception("Товар не найден");

            if (product.Quantity <= 0)
                throw new Exception("Товара нет в наличии");

            var cart = await _cartRepository.GetByUserIdAsync(request.UserId);

            if (cart is null)
            {
                cart = new Cart(request.UserId);
                await _cartRepository.CreateAsync(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            var existingQty = existingItem?.Quantity ?? 0;
            if (existingQty + request.Quantity > product.Quantity)
                throw new Exception("Недостаточно товара в наличии");

            cart.AddItem(product.Id, product.ProductName, product.Price, request.Quantity, product.Weight,
            product.Length, product.Width, product.Height, product.IsLargeSizeProduct);
            await _cartRepository.UpdateAsync(cart);
        }
    }
}