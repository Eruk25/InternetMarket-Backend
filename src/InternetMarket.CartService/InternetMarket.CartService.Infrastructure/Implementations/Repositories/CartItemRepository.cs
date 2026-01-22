using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Domain.Entities;

namespace InternetMarket.CartService.Infrastructure.Implementations.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly CartContext _context;

        public CartItemRepository(CartContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            return _context.SaveChangesAsync();
        }

    }
}