using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.EmailService.Application.DTOs.EmailMetadata;

namespace InternetMarket.EmailService.Application.Abstractions.EmailSender
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMetadata emailMetadata);
    }
}