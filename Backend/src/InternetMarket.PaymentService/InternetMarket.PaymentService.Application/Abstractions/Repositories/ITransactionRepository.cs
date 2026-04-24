using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.Entities;

namespace InternetMarket.PaymentService.Application.Abstractions.Repositories
{
    public interface ITransactionRepository
    {
        Task CreateAsync(Transaction transaction);
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<Transaction?> GetByOrderIdAsync(Guid orderId);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
    }
}