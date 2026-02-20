using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products
{
    public record ProductDto(Guid Id, string Title, string Description,
        decimal Price, int Quantity, string Category, string Provider);
}