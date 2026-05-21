using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Application.Shipments.Calculate;

namespace InternetMarket.ShipmentService.Application.Abstractions.Clients
{
    public interface IShipmentClient
    {
        Task<string> GetTokenAsync();
        Task<string> RefreshTokenAsync();
        Task<IEnumerable<ShipmentCityResponse>?> GetCitiesAsync(string name, CancellationToken cancellationToken = default);
        Task CreateOrderAsync();
        Task<object> GetOrderInfo(Guid id);
        Task<CalculateDeliveryPriceResponse?> CalculateTariffAsync(int toCityCode, int typeOfDelivery, CancellationToken cancellationToken = default);
    }
}