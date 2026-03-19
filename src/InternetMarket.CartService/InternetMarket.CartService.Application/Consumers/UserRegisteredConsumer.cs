using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Application.Carts.Create;
using InternetMarket.CartService.Domain.Entities;
using InternetMarket.Contracts.Events;
using MassTransit;
using MassTransit.Mediator;
using MediatR;

namespace InternetMarket.CartService.Application.Consumers
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
            await _sender.Send(new CreateCartCommand(context.Message.UserId));
        }

    }
}