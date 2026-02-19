using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.CartService.API.DTOs.Requests
{
    public record AddToCartRequest(Guid ProductId, int Quantity);
}