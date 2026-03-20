using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Clients;
using InternetMarket.CartService.Application.DTOs;

namespace InternetMarket.CartService.Infrastructure.Implementations.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
        {
            var payload = new { Ids = new List<Guid> { productId } };
            string jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"by-ids", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
            return products?.FirstOrDefault();
        }
    }
}