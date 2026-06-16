using System;
using InternetMarket.ProductService.Application.Abstractions.Caching;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get
{
    public record GetProductsPagedQuery(int Page, int PageSize) : IRequest<PagedResult<ProductDto>>, ICacheableQuery
    {
        public string CacheKey => Abstractions.Caching.ProductCacheKeys.GetPaged(Page, PageSize);
        public TimeSpan AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    }
}
