using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.DTOs;

namespace InternetMarket.OrderService.Application.Abstractions.Clients
{
    public interface ICartServiceClient
    {
        Task<CartDto?> GetCartByUserIdAsync(Guid userId);
        Task ClearCartAsync(Guid cartId);

    }
}