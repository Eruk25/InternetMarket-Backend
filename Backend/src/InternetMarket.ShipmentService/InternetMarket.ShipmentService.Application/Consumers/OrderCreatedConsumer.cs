using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.ShipmentService.Application.DTOs;
using InternetMarket.ShipmentService.Application.Shipments.Create;
using MassTransit;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ISender _sender;

        public OrderCreatedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            await _sender.Send(new CreateShipmentCommand(
                context.Message.DeliveryType,
                context.Message.ToCityCode,
                context.Message.DeliveryPointId,
                context.Message.City,
                context.Message.Address,
                context.Message.FullName,
                context.Message.NumberPhone,
                context.Message.OrderId,
                context.Message.Items.Select(i => new OrderItemDto(
                    i.ProductId,
                    i.ProductName,
                    i.Quantity,
                    i.UnitPrice,
                    i.Weight,
                    i.Length,
                    i.Width,
                    i.Height,
                    i.IsLargeSizeProduct
                ))
            ));
        }
    }
}