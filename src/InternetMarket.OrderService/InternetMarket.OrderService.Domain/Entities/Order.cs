using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.ValueObjects;

namespace InternetMarket.OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public decimal TotalPrice { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Order(Guid userId)
        {
            UserId = userId;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                _orderItems.Add(item);
            }
            TotalPrice = _orderItems.Sum(oi => oi.TotalPrice);
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Paid)
                throw new ArgumentException("Cannot cancel paid order.");

            Status = OrderStatus.Cancelled;
        }
    }
}