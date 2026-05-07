using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Update.CancelReservation
{
    public class CancelReservationCommanHandler : IRequestHandler<CancelReservationCommand>
    {
        private readonly IProductRepository _productRepository;

        public CancelReservationCommanHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByIdsAsync(request.ItemsToReserve.Keys);

            if (products is null)
                throw new ArgumentNullException("Products was not found.");
            if (products.Count() != request.ItemsToReserve.Count)
                throw new KeyNotFoundException("Several products from order was not found.");
            foreach (var product in products)
            {
                product.CancelReservation(request.ItemsToReserve[product.Id]);
            }
            await _productRepository.UpdateRangeAsync(products);
        }
    }
}