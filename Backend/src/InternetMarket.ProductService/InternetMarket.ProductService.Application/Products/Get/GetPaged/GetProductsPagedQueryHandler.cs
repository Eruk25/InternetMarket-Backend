using InternetMarket.ProductService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get
{
    public class GetProductsPagedQueryHandler : IRequestHandler<GetProductsPagedQuery, PagedResult<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsPagedQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetProductsPagedQuery request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _productRepository.GetPagedAsync(request.Page, request.PageSize, request.SearchTerm);

            var items = products.Select(p => new ProductDto(
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
            )).ToList();

            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            return new PagedResult<ProductDto>(items, totalCount, request.Page, request.PageSize, totalPages);
        }
    }
}
