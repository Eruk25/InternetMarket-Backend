using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.CartService.Domain.Entities;
using MediatR;

namespace InternetMarket.CartService.Application.Carts.Get
{
    public record GetCartByUserIdQuery(Guid UserId) : IRequest<CartDto>;
}