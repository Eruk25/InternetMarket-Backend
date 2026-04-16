using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.CartService.Infrastructure.Implementations.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartContext _context;

        public CartRepository(CartContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetByUserIdAsync(Guid UserId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == UserId);
            return cart;
        }

        public async Task<bool> ExistsByUserIdAsync(Guid UserId)
        {
            return await _context.Carts
                .AnyAsync(c => c.UserId == UserId);
        }

        public async Task CreateAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

    }
}