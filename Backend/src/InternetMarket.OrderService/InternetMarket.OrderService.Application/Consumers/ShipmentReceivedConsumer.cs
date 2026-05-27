using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Shipment;
using InternetMarket.OrderService.Application.Orders.Update.UpdateStatus.ReceivedOrder;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Consumers
{
    public class ShipmentReceivedConsumer : IConsumer<ShipmentReceived>
    {
        private readonly ISender _sender;

        public ShipmentReceivedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<ShipmentReceived> context)
        {
            await _sender.Send(new ReceivedOrderCommand(context.Message.OrderId));
        }
    }
}