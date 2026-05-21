using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
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
            return await _shipmentClient.CalculateTariffAsync(request.ToCityCode, request.TypeOfDelivery);
        }
    }
}