using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Domain.Entities;

namespace InternetMarket.CartService.Application.Abstractions.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart?> GetByUserIdAsync(Guid UserId);
        public Task<bool> ExisteByUserIdAsync(Guid UserId);
        public Task CreateAsync(Cart cart);
        public Task UpdateAsync(Cart cart);
        public Task DeleteAsync(Cart cart);
    }
}