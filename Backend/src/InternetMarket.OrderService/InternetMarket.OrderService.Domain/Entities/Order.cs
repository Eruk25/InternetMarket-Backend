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
        public decimal DeliveryCost { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public DeliveryInfo DeliveryInfo { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public DateTime? PaymentDeadline { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Order() { }
        public Order(Guid userId, FullName customerName, NumberPhone customerNumber, PaymentMethod paymentMethod, DeliveryInfo deliveryInfo, decimal deliveryCost)
        {
            UserId = userId;
            CustomerName = customerName;
            CustomerPhone = customerNumber;
            PaymentMethod = paymentMethod;
            DeliveryInfo = deliveryInfo;
            DeliveryCost = deliveryCost;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
            if (paymentMethod == PaymentMethod.Card)
                PaymentDeadline = DateTime.UtcNow.AddMinutes(20);
        }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                _orderItems.Add(item);
            }
            TotalPrice = _orderItems.Sum(oi => oi.TotalPrice) + DeliveryCost;
        }
        public void Paid()
        {
            if (Status == OrderStatus.Paid) return;
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Нельзя сделать статус Оплачен, если заказ отменен.");
            Status = OrderStatus.Paid;
            PaymentDate = DateTime.UtcNow;
            PaymentDeadline = null;
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

        public void Received()
        {
            if (Status == OrderStatus.Received) return;
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Нельзя сделать статус Получен, если заказ отменен.");
            Status = OrderStatus.Received;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Paid)
                throw new InvalidOperationException("Нельзя отменить оплаченный заказ");
            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}