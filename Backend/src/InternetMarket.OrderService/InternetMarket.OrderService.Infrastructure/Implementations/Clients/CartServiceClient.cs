using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using InternetMarket.OrderService.Application.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace InternetMarket.OrderService.Infrastructure.Implementations.Clients
{
    public class CartServiceClient : ICartServiceClient
    {
        private readonly HttpClient _httpClient;

        public CartServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartDto?> GetCartByUserIdAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"{userId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error occurred while fetching cart: {error}");
            }

            var cart = await response.Content.ReadFromJsonAsync<CartDto>();
            return cart;
        }

        public async Task ClearCartAsync(Guid cartId)
        {
            var response = await _httpClient.DeleteAsync($"clear/{cartId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error occurred while clearing cart: {error}");
            }
        }
    }
}