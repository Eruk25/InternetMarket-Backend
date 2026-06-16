using System.Linq;
using InternetMarket.ProductService.Application.Abstractions.Caching;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace InternetMarket.ProductService.Application.Products.Update.CancelReservation
{
    public class CancelReservationCommanHandler : IRequestHandler<CancelReservationCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;

        public CancelReservationCommanHandler(IProductRepository productRepository, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByIdsAsync(request.ItemsToReserve.Keys);

            if (products is null)
                throw new ArgumentNullException("Товары не найдены");
            if (products.Count() != request.ItemsToReserve.Count)
                throw new KeyNotFoundException("Некоторые товары из заказа не найдены");
            foreach (var product in products)
            {
                product.CancelReservation(request.ItemsToReserve[product.Id]);
            }
            await _productRepository.UpdateRangeAsync(products);

            foreach (var product in products)
            {
                await _cache.RemoveAsync(ProductCacheKeys.GetById(product.Id), cancellationToken);
            }
            await _cache.RemoveAsync(ProductCacheKeys.GetAll, cancellationToken);
        }
    }
}