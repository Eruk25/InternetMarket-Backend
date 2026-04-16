using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events.Order
{
    public record OrderCancelled(Guid OrderId, string Email);
}