using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.CartService.Application.CartItems
{
    public record CartItemDto(Guid ProductId, string Title, decimal Price, int Quantity);
}