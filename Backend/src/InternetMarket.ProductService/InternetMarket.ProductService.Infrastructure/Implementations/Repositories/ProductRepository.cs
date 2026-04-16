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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Provider)
                .ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Provider)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var products = await _context.Products
                .Where(p => ids.Contains(p.Id))
                .Include(p => p.Category)
                .Include(p => p.Provider)
                .ToListAsync();
            return products;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}