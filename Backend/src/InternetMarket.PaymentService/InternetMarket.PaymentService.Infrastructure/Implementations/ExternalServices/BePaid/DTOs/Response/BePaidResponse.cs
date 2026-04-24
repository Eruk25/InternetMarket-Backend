using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Response
{
    public class BePaidResponse
    {
        [JsonPropertyName("checkout")]
        public Checkout Checkout { get; set; } = default!;
    }
}