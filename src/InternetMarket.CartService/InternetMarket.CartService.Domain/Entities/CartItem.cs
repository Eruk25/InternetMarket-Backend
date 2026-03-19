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
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CartItem(Guid productId, string title, decimal price, int quantity)
        {
            ProductId = productId;
            Title = title;
            Price = price;
            Quantity = quantity;
        }
    }
}