using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Create
{
    public record CreateOrderCommand(Guid UserId, string NumberPhone, string Street,
     string City, string ZipCode) : IRequest;
}