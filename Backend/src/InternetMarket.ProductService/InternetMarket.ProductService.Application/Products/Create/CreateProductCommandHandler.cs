using InternetMarket.ProductService.Application.Abstractions.Caching;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using InternetMarket.ProductService.Domain.Entities;
using InternetMarket.ProductService.Domain.ValueObjects;
using InternetMarket.ProductService.Domain.ValueObjects.Product;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace InternetMarket.ProductService.Application.Products.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IDistributedCache _cache;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProviderRepository providerRepository,
            IDistributedCache cache)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _providerRepository = providerRepository;
            _cache = cache;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new ArgumentException($"Категория с id {request.CategoryId} не найдена");

            var provider = await _providerRepository.GetByIdAsync(request.ProviderId);
            if (provider == null)
                throw new ArgumentException($"Поставщик с id {request.ProviderId} не найден");

            var existing = await _productRepository.GetByNameAsync(request.ProductName);

            if (existing is not null)
            {
                existing.AddQuantity(request.Quantity);
                await _productRepository.UpdateAsync(existing);
                await _cache.RemoveAsync(ProductCacheKeys.GetAll, cancellationToken);
                return new ProductDto(
                    existing.Id,
                    existing.ProductName.Value,
                    existing.Description.Value,
                    existing.Price.Value,
                    existing.AvailableQuantity.Value,
                    category.CategoryName.Value,
                    provider.Name.Value,
                    existing.Weight.Value,
                    existing.Length.Value,
                    existing.Width.Value,
                    existing.Height.Value,
                    existing.IsLargeSizeProduct,
                    existing.ImageUrl);
            }

            var product = new Product(
                ProductName.Create(request.ProductName),
                Description.Create(request.Description),
                Price.Create(request.Price),
                Quantity.Create(request.Quantity),
                Weight.Create(request.Weight),
                Length.Create(request.Length),
                Width.Create(request.Width),
                Height.Create(request.Height),
                request.CategoryId,
                request.ProviderId,
                request.ImageUrl);

            await _productRepository.CreateAsync(product);

            await _cache.RemoveAsync(ProductCacheKeys.GetAll, cancellationToken);

            return new ProductDto(
                product.Id,
                product.ProductName.Value,
                product.Description.Value,
                product.Price.Value,
                product.AvailableQuantity.Value,
                category.CategoryName.Value,
                provider.Name.Value,
                product.Weight.Value,
                product.Length.Value,
                product.Width.Value,
                product.Height.Value,
                product.IsLargeSizeProduct,
                product.ImageUrl);
        }
    }
}
