using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Domain.Entities;
using InternetMarket.OrderService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.OrderService.Infrastructure.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderContext _orderContext;

        public UserRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }
        public async Task CreateAsync(User user)
        {
            await _orderContext.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            _orderContext.Users.Remove(user);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _orderContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _orderContext.Users.Update(user);
        }
    }
}