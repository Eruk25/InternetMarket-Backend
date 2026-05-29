using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.DTOs;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Create
{
    public record CreateShipmentCommand(string PaymentMethod, int DeliveryType, int ToCityCode, string? DeliveryPointId, string City, string Address,
     string FullName, string NumberPhone, Guid OrderId, IEnumerable<OrderItemDto> OrderItems, decimal TotalPrice) : IRequest<CreateOrderDeliveryResponse>;
}