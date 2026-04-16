using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Password;
using InternetMarket.EmailService.Application.Abstractions.EmailSender;
using InternetMarket.EmailService.Application.DTOs.EmailMetadata;
using MassTransit;

namespace InternetMarket.EmailService.Application.Consumers.Password
{
    public class PasswordResetRequestedConsumer : IConsumer<PasswordResetRequested>
    {
        private readonly IEmailSender _emailSender;

        public PasswordResetRequestedConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<PasswordResetRequested> context)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                context.Message.Email,
                "Reset password. InternetMarket",
                $"""For password resetting click <a href="{context.Message.ResetLink}">here</a>"""
            );
            await _emailSender.SendAsync(emailMetadata);
        }
    }
}