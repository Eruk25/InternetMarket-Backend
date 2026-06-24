using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get
{
    public record GetProductsPagedQuery(int Page, int PageSize, string? SearchTerm = null) : IRequest<PagedResult<ProductDto>>;
}
