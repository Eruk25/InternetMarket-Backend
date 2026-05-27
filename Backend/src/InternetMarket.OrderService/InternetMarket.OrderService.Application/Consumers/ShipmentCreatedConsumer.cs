using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Shipment;
using InternetMarket.OrderService.Application.Orders.Update.UpdateStatus.ShippedOrder;
using MassTransit;
using MassTransit.Middleware;
using MediatR;

namespace InternetMarket.OrderService.Application.Consumers
{
    public class ShipmentCreatedConsumer : IConsumer<ShipmentCreated>
    {
        private readonly ISender _sender;

        public ShipmentCreatedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<ShipmentCreated> context)
        {
            await _sender.Send(new ShippedOrderCommand(context.Message.OrderId));
        }
    }
}