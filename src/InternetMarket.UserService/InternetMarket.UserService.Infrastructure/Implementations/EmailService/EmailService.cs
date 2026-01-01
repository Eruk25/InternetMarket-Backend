using FluentEmail.Core;
using InternetMarket.UserService.Application.Abstractions.EmailSender;
using InternetMarket.UserService.Application.DTOs.EmailMetadata;
namespace InternetMarket.UserService.Infrastructure.Implementations.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail
                ?? throw new ArgumentNullException(nameof(fluentEmail));
        }

        public async Task SendAsync(EmailMetadata emailMetadata)
        {
            await _fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body)
                .SendAsync();
        }
    }
}