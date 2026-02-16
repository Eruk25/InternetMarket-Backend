using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.CartService.Application.Cart.Create
{
    public record CreateCartCommand(Guid UserId) : IRequest;
}