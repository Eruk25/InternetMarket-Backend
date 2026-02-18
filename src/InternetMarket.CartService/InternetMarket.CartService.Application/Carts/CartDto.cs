using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.CartItems;
using InternetMarket.CartService.Domain.Entities;

namespace InternetMarket.CartService.Application.Carts
{
    public record CartDto(IEnumerable<CartItemDto> CartItems);
}