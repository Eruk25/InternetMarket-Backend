using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Application.Shipments.Calculate;
using InternetMarket.ShipmentService.Application.Shipments.Create;
using InternetMarket.ShipmentService.Application.Shipments.Get;
using InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints;
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
        private readonly PackagePacker _packagePacker;

        public ShipmentClient(IDistributedCache cache, IOptions<CdekOptions> options, HttpClient httpClient, PackagePacker packagePacker)
        {
            _cache = cache;
            _options = options.Value;
            _httpClient = httpClient;
            _packagePacker = packagePacker;
        }

        public async Task<CalculateDeliveryPriceResponse?> CalculateTariffAsync(int toCityCode, DeliveryType deliveryType, IEnumerable<OrderItemDto> orderItems, CancellationToken cancellationToken = default)
        {
            var request = new CalculateDeliveryPriceRequest
            {
                Type = 1,
                Currency = 7,
                TariffCode = deliveryType == DeliveryType.OrderPickupPoint ? 483 : 482,
                FromLocation = new ShipmentLocation
                {
                    Code = 9220
                },
                ToLocation = new ShipmentLocation
                {
                    Code = toCityCode
                },
                Packages = _packagePacker.FormPackage(orderItems, false)
            };
            var token = await GetTokenAsync();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "calculator/tariff");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpRequest.Content = JsonContent.Create(request);
            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
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

            if (deliveryType == DeliveryType.CourierDelivery)
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

        public async Task<CreateOrderDeliveryResponse> CreateOrderAsync(PaymentMethod paymentMethod, int? toCityCode, string? deliveryPointId, DeliveryType deliveryType, string? City, string? address, string fullName, string numberPhone, IEnumerable<OrderItemDto> orderItems, CancellationToken cancellationToken = default)
        {
            int tariffCode = deliveryType == DeliveryType.OrderPickupPoint ? 483 : 482;
            var request = new CreateOrderRequest
            {
                Type = 1,
                TariffCode = tariffCode,
                FromLocation = new ShipmentLocation
                {
                    Code = 9220,
                    City = "Минск",
                    Address = "улица Немига, 46"
                },
                Recipient = new Recipient
                {
                    FullName = fullName,
                    Phones = new List<Phone>
                    {
                        new Phone { Number = numberPhone}
                    }
                },
                Packages = _packagePacker.FormPackage(orderItems, paymentMethod == PaymentMethod.Cash ? true : false)
            };

            if (deliveryType == DeliveryType.OrderPickupPoint)
            {
                if (string.IsNullOrWhiteSpace(deliveryPointId))
                    throw new ArgumentException("Для доставки на ПВЗ необзодим ID пункта выдачи", nameof(deliveryPointId));
                request.DeliveryPoint = deliveryPointId;
                request.ToLocation = null;
            }
            else
            {
                if (toCityCode == null)
                    throw new ArgumentException("Для курьерской доставки необходим код города назначения.", nameof(toCityCode));
                request.ToLocation = new ShipmentLocation
                {

                    Code = toCityCode.Value,
                    City = City,
                    Address = address
                };
                request.DeliveryPoint = null;
            }

            var token = await GetTokenAsync();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "orders");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            httpRequest.Content = JsonContent.Create(request);
            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            response.EnsureSuccessStatusCode();

            var orderInfo = await response.Content.ReadFromJsonAsync<CdekCreateOrderDeliveryResponse>(cancellationToken);
            if (orderInfo is null)
                throw new ArgumentException("Ответ от СДЭК пустой.");

            return new CreateOrderDeliveryResponse
            {
                ShipmentOrderId = orderInfo.Entity.ShipmentOrderId,
                State = orderInfo.Requests.FirstOrDefault()!.State
            };
        }

        public async Task<IEnumerable<ShipmentCityResponse>?> GetCitiesAsync(string name, CancellationToken cancellationToken = default)
        {
            var token = await GetTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, $"location/suggest/cities?name={name}&country_code=BY");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var cities = await response.Content.ReadFromJsonAsync<IEnumerable<CdekShipmentCityResponse>>(cancellationToken);
            if (cities is null)
                throw new ArgumentException("Ответ от СДЭК пустой.");

            return cities.Select(c => new ShipmentCityResponse
            {
                Code = c.Code,
                FullName = c.FullName
            });
        }

        public async Task<IEnumerable<DeliveryPointResponse>?> GetDeliveryPointsAsync(int cityCode, CancellationToken cancellationToken = default)
        {
            var token = await GetTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, $"deliverypoints?city_code={cityCode}&type=PVZ");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var deliveryPoints = await response.Content.ReadFromJsonAsync<IEnumerable<CdekDeliveryPointResponse>>(cancellationToken);
            if (deliveryPoints is null)
                throw new ArgumentException("Ответ от СДЭК пустой.");

            return deliveryPoints.Select(dp => new DeliveryPointResponse
            {
                DeliveryCode = dp.DeliveryCode,
                Location = new DeliveryLocation
                {
                    CityCode = dp.Location.CityCode,
                    City = dp.Location.City,
                    Address = dp.Location.Address
                }
            });
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