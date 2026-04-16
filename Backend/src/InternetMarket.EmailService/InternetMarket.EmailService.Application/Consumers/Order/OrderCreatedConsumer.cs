using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.EmailService.Application.Abstractions.EmailSender;
using InternetMarket.EmailService.Application.DTOs.EmailMetadata;
using MassTransit;
using MassTransit.NewIdProviders;

namespace InternetMarket.EmailService.Application.Consumers.Order
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IEmailSender _emailSender;

        public OrderCreatedConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var itemshtml = string.Join("", context.Message.Items.Select(oi =>
                $"<li>{oi.Title} - {oi.Quantity} шт. (цена: {oi.UnitPrice:C})</li>"));

            EmailMetadata emailMetadata = new EmailMetadata(
                context.Message.Email,
                $"Заказ № {context.Message.OrderId} создан",
                $@"<h2>Спасибо! Ваш заказ № {context.Message.OrderId} оформлен</h2>
                <h3>Купленные товары</h3>
                <ol>
                {itemshtml}
                </ol> 
                <p><strong>Итого: {context.Message.TotalPrice:C}</strong></p>"
            );

            await _emailSender.SendAsync(emailMetadata);
        }
    }
}