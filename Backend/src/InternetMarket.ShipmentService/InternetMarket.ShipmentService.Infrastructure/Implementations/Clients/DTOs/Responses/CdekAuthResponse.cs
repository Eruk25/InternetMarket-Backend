using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients
{
    public class CdekAuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = default!;
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}