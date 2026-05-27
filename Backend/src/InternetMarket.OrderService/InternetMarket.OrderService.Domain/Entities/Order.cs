using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        public FullName CustomerName { get; private set; }
        public NumberPhone CustomerPhone { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Order(Guid userId, FullName customerName, NumberPhone customerNumber)
        {
            UserId = userId;
            CustomerName = customerName;
            CustomerPhone = customerNumber;
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
        public void Paid()
        {
            if (Status == OrderStatus.Paid) return;
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Cannot paid cancelled order");
            Status = OrderStatus.Paid;
            PaymentDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Shipped()
        {
            if (Status == OrderStatus.Shipped) return;
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Нельзя сделать статус в Доставке, если заказ отменен.");
            Status = OrderStatus.Shipped;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Paid)
                throw new InvalidOperationException("Cannot cancel Paid order");
            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}