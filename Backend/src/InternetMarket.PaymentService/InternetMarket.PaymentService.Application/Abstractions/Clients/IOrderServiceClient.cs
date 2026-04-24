using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.DTOs;

namespace InternetMarket.PaymentService.Application.Abstractions.Clients
{
    public interface IOrderServiceClient
    {
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
    }
}