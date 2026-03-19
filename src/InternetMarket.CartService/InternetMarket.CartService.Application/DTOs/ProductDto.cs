using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.CartService.Application.DTOs
{
    public record ProductDto(Guid Id, string Title, decimal Price);
}