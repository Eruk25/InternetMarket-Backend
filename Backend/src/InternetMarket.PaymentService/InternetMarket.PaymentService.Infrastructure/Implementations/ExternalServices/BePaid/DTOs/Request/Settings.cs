using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs
{
    public class Settings
    {
        [JsonPropertyName("return_url")]
        public string ReturnUrl { get; set; } = default!;
        [JsonPropertyName("success_url")]
        public string SuccessUrl { get; set; } = default!;
        [JsonPropertyName("decline_url")]
        public string DeclineUrl { get; set; } = default!;
        [JsonPropertyName("fail_url")]
        public string FailUrl { get; set; } = default!;
        [JsonPropertyName("cancel_url")]
        public string CancelUrl { get; set; } = default!;
        [JsonPropertyName("notification_url")]
        public string NotificationUrl { get; set; } = default!;
        [JsonPropertyName("button_next_text")]
        public string ButtonNextText { get; set; } = default!;
        [JsonPropertyName("auto_pay")]
        public bool AutoPay { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; } = default!;
        [JsonPropertyName("customer_fields")]
        public CustomerFields CustomerFields { get; set; } = default!;
        [JsonPropertyName("payment_method")]
        public PaymentType PaymentType { get; set; } = default!;
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; } = default!;
    }
}