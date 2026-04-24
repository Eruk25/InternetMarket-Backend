using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Response
{
    public class Checkout
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = default!;
        [JsonPropertyName("redirect_url")]
        public string RedirectUrl { get; set; } = default!;
    }
}