using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p =>
            new ProductDto(
                p.Id,
                p.ProductName.Value,
                p.Description.Value,
                p.Price.Value,
                p.Quantity.Value,
                p.Category!.CategoryName,
                p.Provider!.Name.Value))
                .ToList();
        }
    }
}