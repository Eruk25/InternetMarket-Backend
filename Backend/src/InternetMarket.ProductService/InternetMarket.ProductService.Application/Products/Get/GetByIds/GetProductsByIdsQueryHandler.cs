using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get.GetByIds
{
    public class GetProductsByIdsQueryHandler : IRequestHandler<GetProductsByIdsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByIdsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByIdsAsync(request.Ids);

            return products.Select(p => new ProductDto(
                p.Id,
                p.ProductName.Value,
                p.Description.Value,
                p.Price.Value,
                p.AvailableQuantity.Value,
                p.Category!.CategoryName.Value,
                p.Provider!.Name.Value,
                p.Weight.Value,
                p.Length.Value,
                p.Width.Value,
                p.Height.Value,
                p.IsLargeSizeProduct,
                p.ImageUrl
            ));
        }
    }
}