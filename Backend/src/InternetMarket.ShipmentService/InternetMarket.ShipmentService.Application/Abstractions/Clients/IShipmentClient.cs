using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Application.Shipments.Calculate;
using InternetMarket.ShipmentService.Application.Shipments.Create;
using InternetMarket.ShipmentService.Application.Shipments.Get;
using InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints;
using InternetMarket.ShipmentService.Domain.ValueObjects;

namespace InternetMarket.ShipmentService.Application.Abstractions.Clients
{
    public interface IShipmentClient
    {
        Task<string> GetTokenAsync();
        Task<string> RefreshTokenAsync();
        Task<IEnumerable<ShipmentCityResponse>?> GetCitiesAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<DeliveryPointResponse>?> GetDeliveryPointsAsync(int cityCode, CancellationToken cancellationToken = default);
        Task<CreateOrderDeliveryResponse> CreateOrderAsync(PaymentMethod paymentMethod, int? toCityCode, string? deliveryPointId, DeliveryType deliveryType, string? City, string? address, string fullName, string numberPhone, IEnumerable<OrderItemDto> orderItems, CancellationToken cancellationToken = default);
        Task<CalculateDeliveryPriceResponse?> CalculateTariffAsync(int toCityCode, DeliveryType deliveryType, IEnumerable<OrderItemDto> orderItems, CancellationToken cancellationToken = default);
    }
}