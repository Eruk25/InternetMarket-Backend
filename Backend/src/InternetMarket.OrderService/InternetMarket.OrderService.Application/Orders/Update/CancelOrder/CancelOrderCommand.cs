using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Delete
{
    public record CancelOrderCommand(Guid OrderId, string Email) : IRequest;
}