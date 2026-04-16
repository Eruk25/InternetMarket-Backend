using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Application.DTOs
{
    public record CartDto(Guid CartId, IEnumerable<CartItemDto> CartItems);
}