using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Application.Shipments.Calculate;
using InternetMarket.ShipmentService.Domain.ValueObjects;
using InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests;
using InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients
{
    public class ShipmentClient : IShipmentClient
    {
        private readonly IDistributedCache _cache;
        private readonly string TokenCacheKey = "cdek_access_token";
        private readonly CdekOptions _options;
        private readonly HttpClient _httpClient;

        public ShipmentClient(IDistributedCache cache, IOptions<CdekOptions> options, HttpClient httpClient)
        {
            _cache = cache;
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task<CalculateDeliveryPriceResponse?> CalculateTariffAsync(int toCityCode, int typeOfDelivery, CancellationToken cancellationToken = default)
        {
            if (typeOfDelivery is not (482 or 483))
                return null;
            var token = await GetTokenAsync();
            var request = new CalculateDeliveryPriceRequest
            {
                Type = 1,
                Currency = 7,
                TariffCode = typeOfDelivery == DeliveryType.OrderPickupPoint.Value ? 483 : 482,
                FromLocation = new ShipmentLocation
                {
                    Code = 9220
                },
                ToLocation = new ShipmentLocation
                {
                    Code = toCityCode
                },
                Packages = new List<Package>
                {
                    new Package
                    {
                        Weight = 1000
                    }
                }
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("calculator/tariff", request);
            if (response.IsSuccessStatusCode)
            {
                var deliveryInfo = await response.Content.ReadFromJsonAsync<CdekTariffResponse>(cancellationToken);
                if (deliveryInfo is null)
                    throw new ArgumentNullException("DeliveryInfo is empty");

                var formattedDate = $"{DateTime.Parse(deliveryInfo.DeliveryDate.Min):d} - {DateTime.Parse(deliveryInfo.DeliveryDate.Max):d}";
                return new CalculateDeliveryPriceResponse
                {
                    TotalSum = deliveryInfo.TotalSum,
                    PeriodMin = deliveryInfo.PeriodMin,
                    PeriodMax = deliveryInfo.PeriodMax,
                    FormattedDate = formattedDate
                };
            }

            if (typeOfDelivery == DeliveryType.CourierDelivery.Value)
            {
                return new CalculateDeliveryPriceResponse
                {
                    TotalSum = 14.5m,
                    PeriodMin = 3,
                    PeriodMax = 5,
                    FormattedDate = $"{DateTime.Today.AddDays(3):d}-{DateTime.Today.AddDays(5):d}"
                };
            }

            return null;
        }

        public Task CreateOrderAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShipmentCityResponse>?> GetCitiesAsync(string name, CancellationToken cancellationToken = default)
        {
            var token = await GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"location/suggest/cities?name={name}&country_code=BY");
            response.EnsureSuccessStatusCode();

            var cities = await response.Content.ReadFromJsonAsync<IEnumerable<ShipmentCityResponse>>(cancellationToken);

            return cities;
        }

        public Task<object> GetOrderInfo(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTokenAsync()
        {
            var cachedToken = await _cache.GetStringAsync(TokenCacheKey);

            if (cachedToken is not null)
            {
                return cachedToken;
            }

            return await RefreshTokenAsync();
        }

        public async Task<string> RefreshTokenAsync()
        {
            var authRequest = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", _options.GrantType),
                new KeyValuePair<string, string>("client_id", _options.ClientId),
                new KeyValuePair<string, string>("client_secret", _options.ClientSecret)
            });
            var response = await _httpClient.PostAsync("oauth/token", authRequest);
            response.EnsureSuccessStatusCode();

            var authData = await response.Content.ReadFromJsonAsync<CdekAuthResponse>();
            if (authData is null)
                throw new ArgumentNullException("AuthData is null");

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(authData.ExpiresIn - 300)
            };

            await _cache.SetStringAsync(TokenCacheKey, authData.AccessToken, cacheOptions);

            return authData.AccessToken;
        }
    }
}