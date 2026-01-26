using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;

namespace InternetMarket.ProductService.Application.Abstractions.Repositories
{
    public interface IProviderRepository
    {
        public Task<IEnumerable<Provider>> GetAllAsync();
        public Task<Provider> GetByIdAsync(Guid id);
        public Task CreateAsync(Provider provider);
        public Task UpdateAsync(Provider provider);
        public Task DeleteAsync(Provider provider);
    }
}