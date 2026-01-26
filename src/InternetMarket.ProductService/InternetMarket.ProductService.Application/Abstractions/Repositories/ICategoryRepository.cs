using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;

namespace InternetMarket.ProductService.Application.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task CreateAsync(Category category);
        public Task UpdateAsync(Category category);
        public Task DeleteAsync(Category category);
    }
}