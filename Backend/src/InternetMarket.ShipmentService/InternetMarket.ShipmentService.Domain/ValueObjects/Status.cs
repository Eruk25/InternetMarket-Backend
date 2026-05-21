using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace InternetMarket.ShipmentService.Domain.ValueObjects
{
    public class Status : SmartEnum<Status>
    {
        public static readonly Status Shipped = new Status(nameof(Shipped), 1);
        public static readonly Status Received = new Status(nameof(Received), 2);

        public Status(string name, int value) : base(name, value) { }
    }
}