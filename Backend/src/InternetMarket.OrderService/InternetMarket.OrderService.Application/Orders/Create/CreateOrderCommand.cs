using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Create
{
    public record CreateOrderCommand(Guid UserId, string PaymentMethod, int DeliveryType, int ToCityCode, string? DeliveryPointId, string City, string Address,
     string FullName, string NumberPhone) : IRequest;
}