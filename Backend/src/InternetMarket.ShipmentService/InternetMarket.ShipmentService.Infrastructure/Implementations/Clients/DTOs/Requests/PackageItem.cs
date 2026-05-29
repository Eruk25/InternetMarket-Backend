using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class PackageItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("ware_key")]
        public string WareKey { get; set; }
        [JsonPropertyName("payment")]
        public Payment Payment { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        public PackageItem(string name, string wareKey, int weight, int amount, decimal cost, Payment payment)
        {
            Name = name;
            WareKey = wareKey;
            Payment = payment;
            Weight = weight;
            Amount = amount;
            Cost = cost;
        }
    }
}