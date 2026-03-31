using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public Guid ProviderId { get; private set; }
        public Provider? Provider { get; private set; }

        public Product(string productName, string description, decimal price, int quantity, Guid categoryId, Guid providerId)
        {
            ProductName = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            ProviderId = providerId;
        }
    }
}