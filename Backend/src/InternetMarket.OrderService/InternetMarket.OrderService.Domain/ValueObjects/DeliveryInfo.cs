using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Domain.ValueObjects
{
    public class DeliveryInfo
    {
        public int DeliveryType { get; set; }
        public int ToCityCode { get; set; }
        public string? DeliveryPointId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public DeliveryInfo(int deliveryType, int toCityCode, string? deliveryPointId, string city, string address)
        {
            DeliveryType = deliveryType;
            ToCityCode = toCityCode;
            DeliveryPointId = deliveryPointId;
            City = city;
            Address = address;
        }
    }
}