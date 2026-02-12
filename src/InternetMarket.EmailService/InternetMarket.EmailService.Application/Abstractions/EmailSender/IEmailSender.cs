using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.EmailService.Application.Abstractions.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}