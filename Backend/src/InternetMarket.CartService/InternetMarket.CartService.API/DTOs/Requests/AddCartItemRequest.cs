using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.CartService.API.DTOs.Requests
{
    public record AddCartItemRequest(Guid ProductId, int Quantity);
}