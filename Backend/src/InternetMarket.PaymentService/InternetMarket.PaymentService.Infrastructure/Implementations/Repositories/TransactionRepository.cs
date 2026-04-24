using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.Repositories;
using InternetMarket.PaymentService.Domain.Entities;
using InternetMarket.PaymentService.Domain.ValueObjects;
using InternetMarket.PaymentService.Infrastructure.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentContext _paymentContext;

        public TransactionRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await _paymentContext.Transactions
                .AddAsync(transaction);
        }
        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            var transaction = await _paymentContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == id);
            return transaction;
        }
        public async Task<Transaction?> GetByOrderIdAsync(Guid orderId)
        {
            var transaction = await _paymentContext.Transactions
                .Where(t => t.OrderId == orderId && t.Status == Status.Pending)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();
            return transaction;
        }
        public async Task DeleteAsync(Transaction transaction)
        {
            _paymentContext.Transactions.Remove(transaction);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _paymentContext.Transactions.Update(transaction);
        }
    }
}