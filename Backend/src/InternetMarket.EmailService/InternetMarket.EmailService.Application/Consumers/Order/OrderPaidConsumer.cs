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
    public class OrderPaidConsumer : IConsumer<OrderPaid>
    {
        private readonly IEmailSender _emailSender;

        public OrderPaidConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<OrderPaid> context)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                context.Message.Email,
                $"Ваш заказ № {context.Message.OrderId} был оплачен",
                "Спасибо за заказ!");
            await _emailSender.SendAsync(emailMetadata);
        }
    }
}