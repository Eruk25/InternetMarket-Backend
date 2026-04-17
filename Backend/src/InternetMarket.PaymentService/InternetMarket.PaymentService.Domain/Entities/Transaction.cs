using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.ValueObjects;

namespace InternetMarket.PaymentService.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Status Status { get; private set; }
        public decimal Amount { get; private set; }
        public Guid PaymentId { get; private set; }
        public PaymentMethod? PaymentMethod { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Transaction(decimal amount, Guid paymentId)
        {
            Status = Status.Pending;
            Amount = amount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}