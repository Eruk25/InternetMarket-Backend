using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class Payment
    {
        [JsonPropertyName("value")]
        public float Value { get; set; }

        public Payment(float value)
        {
            Value = value;
        }
    }
}