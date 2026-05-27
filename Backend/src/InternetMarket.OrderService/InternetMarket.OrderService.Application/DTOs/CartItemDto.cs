using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Application.DTOs
{
    public record CartItemDto(Guid ProductId, string Title, decimal Price, int Quantity, int Weight, int Length, int Width, int Height, bool IsLargeSizeProduct);
}