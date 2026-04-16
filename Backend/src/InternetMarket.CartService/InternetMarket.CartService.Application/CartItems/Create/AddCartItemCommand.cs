using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.CartService.Application.CartItems.Create
{
    public record AddCartItemCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest;
}