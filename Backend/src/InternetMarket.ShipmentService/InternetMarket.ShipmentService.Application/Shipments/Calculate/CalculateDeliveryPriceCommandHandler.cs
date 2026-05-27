using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Domain.ValueObjects;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Calculate
{
    public class CalculateDelivaryPriceCommandHandler : IRequestHandler<CalculateDeliveryPriceCommand, CalculateDeliveryPriceResponse?>
    {
        private readonly IShipmentClient _shipmentClient;

        public CalculateDelivaryPriceCommandHandler(IShipmentClient shipmentClient)
        {
            _shipmentClient = shipmentClient;
        }

        public async Task<CalculateDeliveryPriceResponse?> Handle(CalculateDeliveryPriceCommand request, CancellationToken cancellationToken)
        {
            DeliveryType deliveryType = request.DeliveryType switch
            {
                _ when request.DeliveryType == DeliveryType.OrderPickupPoint.Value => DeliveryType.OrderPickupPoint,
                _ when request.DeliveryType == DeliveryType.CourierDelivery.Value => DeliveryType.CourierDelivery,
                _ => throw new ArgumentException($"Неизветсный тип доставки: {request.DeliveryType}")
            };
            return await _shipmentClient.CalculateTariffAsync(request.ToCityCode, deliveryType, request.OrderItems);
        }
    }
}