using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.Repositories;

namespace InternetMarket.PaymentService.Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITransactionRepository Transactions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}