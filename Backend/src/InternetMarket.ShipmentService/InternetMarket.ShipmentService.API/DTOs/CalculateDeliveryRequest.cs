using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.API.DTOs
{
    public record CalculateDeliveryRequest(int ToCityCode, int TypeOfDelivery);
}