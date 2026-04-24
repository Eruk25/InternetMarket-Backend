using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response
{
    public class PaymentData
    {
        public string Token { get; set; } = default!;
        public string RedirectUrl { get; set; } = default!;
    }
}