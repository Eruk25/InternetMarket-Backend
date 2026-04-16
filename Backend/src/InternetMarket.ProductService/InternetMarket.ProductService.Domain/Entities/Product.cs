using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.ValueObjects;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public ProductName ProductName { get; private set; }
        public Description Description { get; private set; }
        public Price Price { get; private set; }
        public Quantity Quantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public Guid ProviderId { get; private set; }
        public Provider? Provider { get; private set; }

        public Product(ProductName productName, Description description, Price price, Quantity quantity, Guid categoryId, Guid providerId)
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