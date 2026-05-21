using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace InternetMarket.ShipmentService.Domain.ValueObjects
{
    public class DeliveryType : SmartEnum<DeliveryType>
    {
        public static readonly DeliveryType OrderPickupPoint = new DeliveryType(nameof(OrderPickupPoint), 483);
        public static readonly DeliveryType CourierDelivery = new DeliveryType(nameof(CourierDelivery), 482);
        public DeliveryType(string name, int value) : base(name, value) { }
    }
}