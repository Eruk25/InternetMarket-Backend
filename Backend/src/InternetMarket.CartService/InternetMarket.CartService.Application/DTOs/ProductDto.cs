using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.CartService.Application.DTOs
{
    public record ProductDto(Guid Id, string ProductName, decimal Price, int Weight,
     int Length, int Width, int Height, bool IsLargeSizeProduct);
}