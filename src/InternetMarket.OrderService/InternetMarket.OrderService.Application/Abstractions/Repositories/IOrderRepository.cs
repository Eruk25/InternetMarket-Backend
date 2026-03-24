using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.Entities;

namespace InternetMarket.OrderService.Application.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid userId);
        Task<Order?> GetByIdAsync(Guid id);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}