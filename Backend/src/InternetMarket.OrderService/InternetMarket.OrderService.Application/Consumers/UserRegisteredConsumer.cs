using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events;
using InternetMarket.OrderService.Application.Users.Create;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly ISender _sender;

        public UserRegisteredConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            await _sender.Send(new UserCreateCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName));
        }
    }
}