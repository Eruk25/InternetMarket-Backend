using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response.Notification;

namespace InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response
{
    public class BePaidWebhookDto
    {
        [JsonPropertyName("transaction")]
        public BePaidTransaction Transaction { get; set; } = default!;
    }
}