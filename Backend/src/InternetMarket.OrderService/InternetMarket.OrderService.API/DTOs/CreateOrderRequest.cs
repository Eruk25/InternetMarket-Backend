using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.API.DTOs
{
    public record CreateOrderRequest(string NumberPhone, string Street, string City, string ZipCode);
}