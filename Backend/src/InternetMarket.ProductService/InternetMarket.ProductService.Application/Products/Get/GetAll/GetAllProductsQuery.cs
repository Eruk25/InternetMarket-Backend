using System;
using System.Collections.Generic;
using InternetMarket.ProductService.Application.Abstractions.Caching;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>, ICacheableQuery
    {
        public string CacheKey => Abstractions.Caching.ProductCacheKeys.GetAll;
        public TimeSpan AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    }
}