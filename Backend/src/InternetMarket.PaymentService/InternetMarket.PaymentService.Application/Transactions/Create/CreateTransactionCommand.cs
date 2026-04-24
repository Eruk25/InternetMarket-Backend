using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.PaymentService.Application.Transactions.Create
{
    public record CreateTransactionCommand(Guid OrderId) : IRequest<string>;
}