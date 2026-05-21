using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Domain.ValueObjects;

namespace InternetMarket.ShipmentService.Domain.Entities
{
    public class Shipment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Location Location { get; private set; }
        public FullName FullName { get; private set; }
        public NumberPhone NumberPhone { get; private set; }
        public DeliveryType DeliveryType { get; private set; }
        public decimal ShipmentAmount { get; private set; }
        public Status Status { get; private set; }
        private Shipment() { }
        public Shipment(Guid id, Guid orderId, Location location, FullName fullName, NumberPhone numberPhone, DeliveryType deliveryType, decimal shipmentAmount)
        {
            Id = id;
            OrderId = orderId;
            Location = location;
            FullName = fullName;
            NumberPhone = numberPhone;
            DeliveryType = deliveryType;
            ShipmentAmount = shipmentAmount;
            Status = Status.Shipped;
        }

        public void Received()
        {
            if (Status == Status.Received) return;

            Status = Status.Received;
        }
    }
}