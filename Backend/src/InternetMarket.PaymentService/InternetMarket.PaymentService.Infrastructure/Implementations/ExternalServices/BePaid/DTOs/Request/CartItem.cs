using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Request
{
    public class CartItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;
        [JsonPropertyName("nomenclature_code")]
        public string NomenclatureCode { get; set; } = default!;
    }
}