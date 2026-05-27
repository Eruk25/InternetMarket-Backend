using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using InternetMarket.Contracts.Events.Transaction;
using InternetMarket.OrderService.Application.Orders.Update.UpdateStatus;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Consumers
{
    public class TransactionSuccessfulConsumer : IConsumer<TransactionSuccessful>
    {
        private readonly ISender _sender;

        public TransactionSuccessfulConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<TransactionSuccessful> context)
        {
            await _sender.Send(new PayOrderCommand(context.Message.OrderId));
        }
    }
}