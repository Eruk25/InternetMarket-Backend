using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Email;
using InternetMarket.EmailService.Application.Abstractions.EmailSender;
using InternetMarket.EmailService.Application.DTOs.EmailMetadata;
using MassTransit;

namespace InternetMarket.EmailService.Application.Consumers.Email
{
    public class EmailChangeRequestedConsumer : IConsumer<EmailChangeRequested>
    {
        private readonly IEmailSender _emailSender;

        public EmailChangeRequestedConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<EmailChangeRequested> context)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                context.Message.Email,
                "Change Email. InternetMarket",
                $"""For email changing click <a href="{context.Message.VerificationLink}">here</a>""");
            await _emailSender.SendAsync(emailMetadata);
        }
    }
}