using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.PaymentService.Application.Transactions.Update
{
    public record UpdateTransactionStatusCommand(string Json) : IRequest;
}