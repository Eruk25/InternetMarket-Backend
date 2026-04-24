using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.DTOs;
using InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response;

namespace InternetMarket.PaymentService.Application.Abstractions.PaymentGateway
{
    public interface IPaymentGateway
    {
        Task<PaymentData> CreateSessionsAsync(OrderDto orderDto);
        string BuildUrl(string token);
    }
}