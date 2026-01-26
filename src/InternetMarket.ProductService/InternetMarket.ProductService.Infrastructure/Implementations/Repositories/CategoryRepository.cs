using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using InternetMarket.ProductService.Domain.Entities;
using InternetMarket.ProductService.Infrastructure.Persistance.DB;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.ProductService.Infrastructure.Implementations.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductContext _context;

        public CategoryRepository(ProductContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _context.Categories.ToArrayAsync();
            return categories;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}