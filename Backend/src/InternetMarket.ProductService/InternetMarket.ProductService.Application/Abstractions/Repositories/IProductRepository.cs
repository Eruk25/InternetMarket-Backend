using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;

namespace InternetMarket.ProductService.Application.Abstractions.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
        public Task CreateAsync(Product product);
        public Task UpdateAsync(Product product);
        public Task UpdateRangeAsync(IEnumerable<Product> products);
        public Task DeleteAsync(Product product);
        public Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? searchTerm = null);
    }
}