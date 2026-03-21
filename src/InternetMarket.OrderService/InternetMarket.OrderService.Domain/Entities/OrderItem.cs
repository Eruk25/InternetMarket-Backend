using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
        public decimal UntilPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public OrderItem(Guid productId, int quantity, decimal untilPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UntilPrice = untilPrice;
            CreatedAt = DateTime.UtcNow;
        }
    }
}