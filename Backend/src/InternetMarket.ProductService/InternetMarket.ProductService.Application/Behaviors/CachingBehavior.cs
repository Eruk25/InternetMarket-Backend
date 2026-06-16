using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace InternetMarket.ProductService.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly IDistributedCache _cache;

        public CachingBehavior(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cached = await _cache.GetStringAsync(request.CacheKey, cancellationToken);
            if (cached is not null)
                return JsonSerializer.Deserialize<TResponse>(cached, JsonOptions)!;

            var response = await next();

            var json = JsonSerializer.Serialize(response, JsonOptions);
            await _cache.SetStringAsync(request.CacheKey, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = request.AbsoluteExpirationRelativeToNow
            }, cancellationToken);

            return response;
        }
    }
}
