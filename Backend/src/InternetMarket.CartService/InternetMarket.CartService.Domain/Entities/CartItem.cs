using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace InternetMarket.CartService.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; private set; }
        public Guid CartId { get; private set; }
        public Cart? Cart { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public int Weight { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool IsLargeSizeProduct { get; private set; }

        public CartItem(Guid productId, string productName, decimal price, int quantity,
         int weight, int length, int width, int height, bool isLargeSizeProduct)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
            IsLargeSizeProduct = isLargeSizeProduct;
        }

        public void Increase(int quantity)
        {
            Quantity += quantity;
        }
    }
}