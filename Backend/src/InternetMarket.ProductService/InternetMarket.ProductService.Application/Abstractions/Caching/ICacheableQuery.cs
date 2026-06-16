using System;

namespace InternetMarket.ProductService.Application.Abstractions.Caching
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan AbsoluteExpirationRelativeToNow { get; }
    }
}
