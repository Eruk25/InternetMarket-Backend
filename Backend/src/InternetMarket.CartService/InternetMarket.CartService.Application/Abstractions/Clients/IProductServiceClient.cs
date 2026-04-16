using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.DTOs;

namespace InternetMarket.CartService.Application.Abstractions.Clients
{
    public interface IProductServiceClient
    {
        Task<ProductDto?> GetProductByIdAsync(Guid productId);
    }
}