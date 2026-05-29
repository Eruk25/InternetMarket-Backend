using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.PaymentService.Application.PaymentMethods.Get
{
    public class GetPaymentMethodsQueryHandler : IRequestHandler<GetPaymentMethodsQuery, IEnumerable<PaymentMethodDto>>
    {
        private readonly IPaymentMethodRepository _paymentMehtodRepository;

        public GetPaymentMethodsQueryHandler(IPaymentMethodRepository paymentMehtodRepository)
        {
            _paymentMehtodRepository = paymentMehtodRepository;
        }

        public async Task<IEnumerable<PaymentMethodDto>> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await _paymentMehtodRepository.GetAllAsync(cancellationToken);

            return paymentMethods.Select(pm => new PaymentMethodDto(pm.Name, pm.SystemName, pm.IsActive));
        }
    }
}