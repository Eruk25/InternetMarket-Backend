using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses
{
    public class CdekTariffResponse
    {
        [JsonPropertyName("total_sum")]
        public decimal TotalSum { get; set; }
        [JsonPropertyName("period_min")]
        public int PeriodMin { get; set; }
        [JsonPropertyName("period_max")]
        public int PeriodMax { get; set; }
        [JsonPropertyName("delivery_date_range")]
        public DeliveryDateRange DeliveryDate { get; set; } = default!;
    }
}