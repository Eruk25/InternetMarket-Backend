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
        public List<CartItem> CartItems { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Cart(Guid userId)
        {
            UserId = userId;
        }
    }
}