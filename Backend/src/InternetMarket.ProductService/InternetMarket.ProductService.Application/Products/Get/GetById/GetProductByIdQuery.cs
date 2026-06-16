using System;
using InternetMarket.ProductService.Application.Abstractions.Caching;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>, ICacheableQuery
    {
        public string CacheKey => Abstractions.Caching.ProductCacheKeys.GetById(Id);
        public TimeSpan AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    }
}