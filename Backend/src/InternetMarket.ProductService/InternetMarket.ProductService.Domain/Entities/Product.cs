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
        public Quantity PhysicalQuantity { get; private set; }
        public Quantity AvailableQuantity { get; private set; }
        public Quantity ReservedQuantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public Guid ProviderId { get; private set; }
        public Provider? Provider { get; private set; }

        public Product(ProductName productName, Description description, Price price, Quantity physicalQuantity, Guid categoryId, Guid providerId)
        {
            ProductName = productName;
            Description = description;
            Price = price;
            PhysicalQuantity = physicalQuantity;
            AvailableQuantity = physicalQuantity;
            ReservedQuantity = Quantity.Create(0);
            CategoryId = categoryId;
            ProviderId = providerId;
        }

        public void Reserve(int quantity)
        {
            ReservedQuantity = ReservedQuantity.Add(quantity);
            AvailableQuantity = AvailableQuantity.Subtract(quantity);
        }

        public void CancelReservation(int quantity)
        {
            ReservedQuantity = ReservedQuantity.Subtract(quantity);
            AvailableQuantity = AvailableQuantity.Add(quantity);
        }

        public void ConfirmShipment(int quantity)
        {
            PhysicalQuantity = PhysicalQuantity.Subtract(quantity);
            ReservedQuantity = ReservedQuantity.Subtract(quantity);
        }
    }
}