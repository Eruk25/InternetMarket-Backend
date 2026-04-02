using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
                throw new ArgumentNullException($"Product with id {request.Id} was not founded.");

            return new ProductDto(
                product.Id,
                product.ProductName.Value,
                product.Description.Value,
                product.Price.Value,
                product.Quantity.Value,
                product.Category!.CategoryName,
                product.Provider!.Name.Value);
        }
    }
}