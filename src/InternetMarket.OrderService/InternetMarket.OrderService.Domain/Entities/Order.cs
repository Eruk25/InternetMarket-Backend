using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public DateTime? PaymentDate { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Order(Guid userId)
        {
            UserId = userId;
            Status = "status";
            CreatedAt = DateTime.UtcNow;
        }

        public Order() { }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                _orderItems.Add(item);
            }
        }
    }
}