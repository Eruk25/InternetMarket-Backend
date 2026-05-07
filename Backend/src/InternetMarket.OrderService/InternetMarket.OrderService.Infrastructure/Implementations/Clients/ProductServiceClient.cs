using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using MassTransit;

namespace InternetMarket.OrderService.Infrastructure.Implementations.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CancelReservationAsync(Dictionary<Guid, int> itemsToCancelReservation)
        {
            await _httpClient.PostAsJsonAsync("cancel-reservation", new { ItemsToReserve = itemsToCancelReservation });
        }

        public async Task ConfirmShipmentAsync(Dictionary<Guid, int> itemsToConfirmShipment)
        {
            await _httpClient.PostAsJsonAsync("confirm-reservation", new { ItemsToReserve = itemsToConfirmShipment });
        }

        public async Task ReserveAsync(Dictionary<Guid, int> itemsToReserve)
        {
            await _httpClient.PostAsJsonAsync("reserve", new { ItemsToReserve = itemsToReserve });
        }
    }
}