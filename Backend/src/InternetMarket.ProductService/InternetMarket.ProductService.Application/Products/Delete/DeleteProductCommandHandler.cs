using InternetMarket.ProductService.Application.Abstractions.Caching;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace InternetMarket.ProductService.Application.Products.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;

        public DeleteProductCommandHandler(IProductRepository productRepository, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
                throw new ArgumentException($"Товар с id {request.Id} не найден");

            await _productRepository.DeleteAsync(product);

            await _cache.RemoveAsync(ProductCacheKeys.GetById(request.Id), cancellationToken);
            await _cache.RemoveAsync(ProductCacheKeys.GetAll, cancellationToken);

            return Unit.Value;
        }
    }
}
