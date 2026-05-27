using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.DTOs;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Calculate
{
    public record CalculateDeliveryPriceCommand(int DeliveryType, int ToCityCode, IEnumerable<OrderItemDto> OrderItems) : IRequest<CalculateDeliveryPriceResponse?>;
}