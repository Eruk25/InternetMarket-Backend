using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace InternetMarket.OrderService.Domain.ValueObjects
{
    public class OrderStatus : SmartEnum<OrderStatus>
    {
        public static readonly OrderStatus Created = new OrderStatus(nameof(Created), 1);
        public static readonly OrderStatus Paid = new OrderStatus(nameof(Paid), 2);
        public static readonly OrderStatus Shipped = new OrderStatus(nameof(Shipped), 3);
        public static readonly OrderStatus Cancelled = new OrderStatus(nameof(Cancelled), 4);

        public OrderStatus(string name, int value) : base(name, value) { }
    }
}