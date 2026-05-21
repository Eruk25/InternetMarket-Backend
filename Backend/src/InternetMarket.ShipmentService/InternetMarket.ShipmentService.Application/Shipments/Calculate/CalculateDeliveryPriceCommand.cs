using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Calculate
{
    public record CalculateDeliveryPriceCommand(int ToCityCode, int TypeOfDelivery) : IRequest<CalculateDeliveryPriceResponse?>;
}