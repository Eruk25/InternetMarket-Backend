using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.DTOs.EmailMetadata;

namespace InternetMarket.UserService.Application.Abstractions.EmailSender
{
    public interface IEmailService
    {
        public Task SendAsync(EmailMetadata emailMetadata);
    }
}