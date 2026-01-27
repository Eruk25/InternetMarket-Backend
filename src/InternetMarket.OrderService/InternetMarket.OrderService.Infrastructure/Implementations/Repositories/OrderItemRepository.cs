using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Domain.Entities;
using InternetMarket.OrderService.Infrastructure.Persistence;

namespace InternetMarket.OrderService.Infrastructure.Implementations.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderContext _context;

        public OrderItemRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            var orderItems = await _context.OrderItems
                .ToListAsync();
            return orderItems;
        }

        public Task<OrderItem?> GetByIdAsync(Guid id)
        {
            var orderItem = _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == id);
            return orderItem;
        }

        public async Task CreateAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            return _context.SaveChangesAsync();
        }

    }
}