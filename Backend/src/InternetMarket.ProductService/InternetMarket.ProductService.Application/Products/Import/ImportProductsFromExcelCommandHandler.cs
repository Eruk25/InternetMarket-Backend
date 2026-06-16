using InternetMarket.ProductService.Application.Abstractions.Caching;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using InternetMarket.ProductService.Domain.Entities;
using InternetMarket.ProductService.Domain.ValueObjects;
using InternetMarket.ProductService.Domain.ValueObjects.Category;
using InternetMarket.ProductService.Domain.ValueObjects.Product;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace InternetMarket.ProductService.Application.Products.Import
{
    public class ImportProductsFromExcelCommandHandler : IRequestHandler<ImportProductsFromExcelCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IDistributedCache _cache;

        public ImportProductsFromExcelCommandHandler(
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

        public async Task<int> Handle(ImportProductsFromExcelCommand request, CancellationToken cancellationToken)
        {
            var existingCategories = (await _categoryRepository.GetAllAsync()).ToList();
            var existingProviders = (await _providerRepository.GetAllAsync()).ToList();

            var importedCount = 0;

            foreach (var row in request.Products)
            {
                var category = existingCategories.FirstOrDefault(c =>
                    c.CategoryName.Value.Equals(row.CategoryName, StringComparison.OrdinalIgnoreCase));

                if (category == null)
                {
                    category = new Category(CategoryName.Create(row.CategoryName));
                    await _categoryRepository.CreateAsync(category);
                    existingCategories.Add(category);
                }

                var provider = existingProviders.FirstOrDefault(p =>
                    p.Name.Value.Equals(row.ProviderName, StringComparison.OrdinalIgnoreCase));

                if (provider == null)
                {
                    provider = new Provider(
                        ProviderName.Create(row.ProviderName),
                        Address.Create("Default Street", "Default City"),
                        Email.Create("default@gmail.com"),
                        NumberPhone.Create("+375291234567"));
                    await _providerRepository.CreateAsync(provider);
                    existingProviders.Add(provider);
                }

                var product = new Product(
                    ProductName.Create(row.ProductName),
                    Description.Create(string.IsNullOrWhiteSpace(row.Description) ? row.ProductName : row.Description),
                    Price.Create(row.Price),
                    Quantity.Create(row.Quantity),
                    Weight.Create(row.Weight),
                    Length.Create(row.Length),
                    Width.Create(row.Width),
                    Height.Create(row.Height),
                    category.Id,
                    provider.Id,
                    row.ImageUrl);

                await _productRepository.CreateAsync(product);
                importedCount++;
            }

            await _cache.RemoveAsync(ProductCacheKeys.GetAll, cancellationToken);

            return importedCount;
        }
    }
}
