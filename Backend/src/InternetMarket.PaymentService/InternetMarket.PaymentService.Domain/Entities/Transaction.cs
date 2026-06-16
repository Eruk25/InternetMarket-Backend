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
        public string ExternalToken { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid PaymentId { get; private set; }
        public PaymentMethod? PaymentMethod { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime PaymentDate { get; private set; }
        private Transaction() { }
        public Transaction(decimal amount, string externalToken, Guid paymentId, Guid orderId)
        {
            Status = Status.Pending;
            Amount = amount;
            ExternalToken = externalToken;
            OrderId = orderId;
            PaymentId = paymentId;
            CreatedAt = DateTime.UtcNow;
        }

        public void ConfirmSuccess()
        {
            if (Status == Status.Success) return;

            if (Status == Status.Failed)
                throw new InvalidOperationException("Нельзя изменить статус успешной транзакции");
            Status = Status.Success;
            PaymentDate = DateTime.UtcNow;
        }
    }
}