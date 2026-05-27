using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses
{
    public class CdekDeliveryLocation
    {
        [JsonPropertyName("city_code")]
        public int CityCode { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; } = default!;
        [JsonPropertyName("address")]
        public string Address { get; set; } = default!;
    }
}