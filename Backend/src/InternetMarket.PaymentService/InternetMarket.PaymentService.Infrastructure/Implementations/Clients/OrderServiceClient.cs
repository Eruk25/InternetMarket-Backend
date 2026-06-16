using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.Clients;
using InternetMarket.PaymentService.Application.DTOs;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.Clients
{
    public class OrderServiceClient : IOrderServiceClient
    {
        private readonly HttpClient _httpClient;

        public OrderServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var response = await _httpClient.GetAsync($"{orderId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка сервиса заказов: {response.StatusCode}. {error}");
            }

            var order = await response.Content.ReadFromJsonAsync<OrderDto>();

            if (order is null)
                throw new ArgumentNullException("Заказ не найден");

            return order;
        }
    }
}