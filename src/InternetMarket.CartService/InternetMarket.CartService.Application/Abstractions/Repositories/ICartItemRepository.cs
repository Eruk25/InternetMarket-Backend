using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Domain.Entities;

namespace InternetMarket.CartService.Application.Abstractions.Repositories
{
    public interface ICartItemRepository
    {
        public Task CreateAsync(CartItem cartItem);
        public Task UpdateAsync(CartItem cartItem);
        public Task DeleteAsync(Guid id);
    }
}