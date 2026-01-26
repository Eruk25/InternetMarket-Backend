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
        public List<OrderItem> OrderItems { get; private set; } = new();
        public DateTime? PaymentDate { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Order(Guid userId, List<OrderItem> orderItems)
        {
            UserId = userId;
            OrderItems = orderItems;
            Status = "status";
            CreatedAt = DateTime.UtcNow;
        }
    }
}