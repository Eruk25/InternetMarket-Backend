using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events.Shipment
{
    public record ShipmentReceived(Guid OrderId);
}