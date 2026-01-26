using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using InternetMarket.ProductService.Domain.Entities;
using InternetMarket.ProductService.Infrastructure.Migrations;
using InternetMarket.ProductService.Infrastructure.Persistance.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InternetMarket.ProductService.Infrastructure.Implementations.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ProductContext _context;

        public ProviderRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            var providers = await _context.Providers.ToListAsync();
            return providers;
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            var provider = await _context.Providers
                .FirstOrDefaultAsync(p => p.Id == id);
            return provider;
        }

        public async Task CreateAsync(Provider provider)
        {
            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Provider provider)
        {
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
        }

    }
}