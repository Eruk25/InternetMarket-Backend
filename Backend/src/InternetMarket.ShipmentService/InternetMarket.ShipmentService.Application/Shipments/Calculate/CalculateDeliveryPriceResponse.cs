using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Application.Shipments.Calculate
{
    public class CalculateDeliveryPriceResponse
    {
        public decimal TotalSum { get; set; }
        public int PeriodMin { get; set; }
        public int PeriodMax { get; set; }
        public string FormattedDate { get; set; } = string.Empty;
    }
}