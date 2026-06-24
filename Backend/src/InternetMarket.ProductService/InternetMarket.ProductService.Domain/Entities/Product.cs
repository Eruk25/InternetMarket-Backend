using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.ValueObjects;
using InternetMarket.ProductService.Domain.ValueObjects.Category;
using InternetMarket.ProductService.Domain.ValueObjects.Product;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public ProductName ProductName { get; private set; }
        public Description Description { get; private set; }
        public Price Price { get; private set; }
        public Weight Weight { get; private set; }
        public Length Length { get; private set; }
        public Width Width { get; private set; }
        public Height Height { get; private set; }
        public bool IsLargeSizeProduct { get; private set; }
        public Quantity PhysicalQuantity { get; private set; }
        public Quantity AvailableQuantity { get; private set; }
        public Quantity ReservedQuantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public Guid ProviderId { get; private set; }
        public Provider? Provider { get; private set; }
        public string? ImageUrl { get; private set; }

        public Product(ProductName productName, Description description, Price price, Quantity physicalQuantity,
         Weight weight, Length length, Width width, Height height, Guid categoryId, Guid providerId,
         string? imageUrl = null)
        {
            ProductName = productName;
            Description = description;
            Price = price;
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
            IsLargeSizeProduct = height.Value + width.Value + length.Value >= 150;
            PhysicalQuantity = physicalQuantity;
            AvailableQuantity = physicalQuantity;
            ReservedQuantity = Quantity.Create(0);
            CategoryId = categoryId;
            ProviderId = providerId;
            ImageUrl = imageUrl;
        }

        public void AddQuantity(int quantity)
        {
            PhysicalQuantity = PhysicalQuantity.Add(quantity);
            AvailableQuantity = AvailableQuantity.Add(quantity);
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

        public void Update(ProductName productName, Description description, Price price, Quantity quantity,
            Weight weight, Length length, Width width, Height height, Guid categoryId, Guid providerId,
            string? imageUrl = null)
        {
            ProductName = productName;
            Description = description;
            Price = price;
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
            IsLargeSizeProduct = height.Value + width.Value + length.Value >= 150;
            PhysicalQuantity = quantity;
            AvailableQuantity = quantity;
            CategoryId = categoryId;
            ProviderId = providerId;
            ImageUrl = imageUrl;
        }
    }
}