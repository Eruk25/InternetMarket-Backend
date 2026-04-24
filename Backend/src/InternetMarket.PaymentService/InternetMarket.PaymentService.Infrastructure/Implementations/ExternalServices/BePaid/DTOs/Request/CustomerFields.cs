using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs
{
    public class CustomerFields
    {
        [JsonPropertyName("visible")]
        public List<string> Visible { get; set; } = default!;
    }
}