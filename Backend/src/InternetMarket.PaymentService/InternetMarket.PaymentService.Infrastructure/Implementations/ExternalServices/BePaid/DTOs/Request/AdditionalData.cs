using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Request
{
    public class AdditionalData
    {
        [JsonPropertyName("cart")]
        public Cart Cart { get; set; } = default!;
    }
}