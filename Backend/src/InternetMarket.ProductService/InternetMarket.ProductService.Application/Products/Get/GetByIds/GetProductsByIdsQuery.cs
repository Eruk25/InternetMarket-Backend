using System;
using System.Collections.Generic;
using InternetMarket.ProductService.Application.Abstractions.Caching;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Get.GetByIds
{
    public record GetProductsByIdsQuery(IEnumerable<Guid> Ids) : IRequest<IEnumerable<ProductDto>>, ICacheableQuery
    {
        public string CacheKey => Abstractions.Caching.ProductCacheKeys.GetByIds(Ids);
        public TimeSpan AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    }
}