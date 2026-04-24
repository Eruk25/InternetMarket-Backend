using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using InternetMarket.PaymentService.Application.Abstractions.Repositories;
using InternetMarket.PaymentService.Application.Abstractions.UnitOfWork;
using InternetMarket.PaymentService.Infrastructure.Implementations.Repositories;
using InternetMarket.PaymentService.Infrastructure.Persistence.DB;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentContext _paymentContext;
        public ITransactionRepository Transactions { get; }

        public UnitOfWork(PaymentContext paymentContext, ITransactionRepository transactionRepository)
        {
            _paymentContext = paymentContext;
            Transactions = transactionRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _paymentContext.SaveChangesAsync(cancellationToken);
        }
    }
}