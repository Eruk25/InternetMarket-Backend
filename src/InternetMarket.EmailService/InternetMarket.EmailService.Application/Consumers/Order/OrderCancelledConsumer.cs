using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.EmailService.Application.Abstractions.EmailSender;
using InternetMarket.EmailService.Application.DTOs.EmailMetadata;
using MassTransit;

namespace InternetMarket.EmailService.Application.Consumers.Order
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly IEmailSender _emailSender;

        public OrderCancelledConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<OrderCancelled> context)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                context.Message.Email,
                $"Заказ №{context.Message.OrderId} был отменен."
            );

            await _emailSender.SendAsync(emailMetadata);
        }
    }
}