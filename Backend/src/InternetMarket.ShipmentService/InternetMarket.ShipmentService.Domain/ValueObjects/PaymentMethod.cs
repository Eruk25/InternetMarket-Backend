using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace InternetMarket.ShipmentService.Domain.ValueObjects
{
    public class PaymentMethod : SmartEnum<PaymentMethod>
    {
        public static readonly PaymentMethod Cash = new PaymentMethod(nameof(Cash), 0);
        public static readonly PaymentMethod Card = new PaymentMethod(nameof(Card), 1);

        public PaymentMethod(string name, int value) : base(name, value) { }
    }
}