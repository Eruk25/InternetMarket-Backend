using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.UnitOfWork;
using InternetMarket.OrderService.Infrastructure.Persistence;

namespace InternetMarket.OrderService.Infrastructure.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderContext _orderContext;

        public UnitOfWork(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _orderContext.SaveChangesAsync(cancellationToken);
        }
    }
}