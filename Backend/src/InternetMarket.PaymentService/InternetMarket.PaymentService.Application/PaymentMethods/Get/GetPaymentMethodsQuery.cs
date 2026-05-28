using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.PaymentService.Application.PaymentMethods.Get
{
    public record GetPaymentMethodsQuery() : IRequest<IEnumerable<PaymentMethodDto>>;
}