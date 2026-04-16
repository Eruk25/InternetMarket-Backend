using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using InternetMarket.UserService.Infrastructure.Persistence.DB;

namespace InternetMarket.UserService.Infrastructure.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext _userContext;

        public UnitOfWork(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _userContext.SaveChangesAsync(cancellationToken);
        }
    }
}