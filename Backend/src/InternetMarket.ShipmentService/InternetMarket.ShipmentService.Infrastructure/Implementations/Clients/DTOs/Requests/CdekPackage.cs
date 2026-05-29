using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class CdekPackage
    {
        public const int MaxSumOfSidesLimit = 150;
        public const int MaxWeightLimitGr = 50000;
        private readonly List<PackageItem> _items = new();
        [JsonPropertyName("Number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Number { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
        [JsonPropertyName("length")]
        public int Length { get; set; }
        [JsonPropertyName("width")]
        public int Width { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("items")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<PackageItem>? PackageItems
        {
            get => _items.Any() ? _items : null;
            set
            {
                _items.Clear();
                if (value != null) _items.AddRange(value);
            }
        }
        public bool CanAccommodate(int itemSumOfSides, int itemwWeight, int quantity = 1)
        {
            if (!_items.Any()) return true;

            if (Weight + (itemwWeight * quantity) > MaxWeightLimitGr) return false;

            int currentSum = Length + Width + Height;
            if (currentSum + (itemSumOfSides * quantity) > MaxSumOfSidesLimit) return false;

            return true;
        }
        public void AddItem(string productName, string wareKey, int itemWeight, int amount, decimal cost, int height, int width, int length, bool isCashPayment)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество товара не может быть меньше 0.", nameof(amount));

            int incomingSumOfSides = height + width + length;

            if (!CanAccommodate(incomingSumOfSides, itemWeight, amount))
                throw new InvalidOperationException("Товар не помещается в эту посылку по лимитам СДЭК.");

            var existingItems = _items.FirstOrDefault(i => i.WareKey == wareKey);
            if (existingItems != null)
            {
                existingItems.Amount += amount;
            }
            else
            {
                _items.Add(new PackageItem(
                    productName,
                    wareKey,
                    itemWeight,
                    amount,
                    cost,
                    new Payment(isCashPayment ? (float)cost : 0)));
            }

            Weight += itemWeight * amount;
            Length += length * amount;

            Width = Math.Max(Width, width);
            Height = Math.Max(Height, height);
        }
    }
}