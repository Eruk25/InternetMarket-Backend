using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Request
{
    public class Cart
    {
        public List<CartItem> Positions { get; set; } = new();
    }
}