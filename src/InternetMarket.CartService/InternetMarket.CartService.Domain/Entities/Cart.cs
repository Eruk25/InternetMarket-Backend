using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.CartService.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        private readonly List<CartItem> _cartItems = new();
        public IReadOnlyCollection<CartItem> Items => _cartItems.AsReadOnly();
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Cart(Guid userId)
        {
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(Guid productId, string title, decimal price, int quantity)
        {
            var existing = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (existing is not null)
                existing.Quantity += quantity;

            var item = new CartItem(productId, title, price, quantity);

            _cartItems.Add(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}