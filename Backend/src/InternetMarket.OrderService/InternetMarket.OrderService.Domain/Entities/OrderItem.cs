using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public int Weight { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool IsLargeSizeProduct { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public OrderItem(Guid productId, string productName, int quantity, decimal unitPrice,
         int weight, int length, int width, int height, bool isLargeSizeProduct)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
            IsLargeSizeProduct = isLargeSizeProduct;
            CreatedAt = DateTime.UtcNow;
        }
    }
}