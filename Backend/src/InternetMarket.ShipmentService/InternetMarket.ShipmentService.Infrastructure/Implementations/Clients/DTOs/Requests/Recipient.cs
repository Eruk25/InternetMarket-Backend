using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Domain.ValueObjects;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class Recipient
    {
        [JsonPropertyName("name")]
        public string FullName { get; set; } = default!;
        [JsonPropertyName("phones")]
        public IEnumerable<Phone> Phones { get; set; } = default!;
    }
}