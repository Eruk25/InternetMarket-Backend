using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Application.DTOs
{
    public record OrderItemDto
    {
        public Guid ProductId { get; }
        public string ProductName { get; } = default!;
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public int Weight { get; }
        public int Length { get; }
        public int Width { get; }
        public int Height { get; }
        public bool IsLargeSizeProduct { get; }

        public OrderItemDto(Guid productId, string productName, int quantity, decimal unitPrice, int weight, int length, int width, int height, bool isLargeSizeProduct)
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
        }
    }
}