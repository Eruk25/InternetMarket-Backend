using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.EmailService.Application.Abstractions.EmailSender;
using InternetMarket.EmailService.Application.DTOs.EmailMetadata;
using InternetMarket.Contracts.Events;
using MassTransit;

namespace InternetMarket.EmailService.Application.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly IEmailSender _emailSender;

        public UserRegisteredConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                context.Message.Email,
                "Hello! From InternetMerket",
                $"""To verify your email click <a href="{context.Message.VerificationLink}">here</a>"""
            );
            await _emailSender.SendAsync(emailMetadata);
        }
    }
}