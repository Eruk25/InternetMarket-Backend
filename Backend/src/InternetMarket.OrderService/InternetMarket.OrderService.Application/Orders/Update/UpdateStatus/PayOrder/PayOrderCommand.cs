using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Update.UpdateStatus
{
    public record PayOrderCommand(Guid OrderId) : IRequest;
}