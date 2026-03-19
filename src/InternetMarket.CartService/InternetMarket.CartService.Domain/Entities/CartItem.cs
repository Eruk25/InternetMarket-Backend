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
        public string ProductName { get; set; }
        public decimal UntilPrice { get; set; }
        public int Quantity { get; set; }

        public CartItem(Guid productId, string productName, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
        }
    }
}