using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.Entities;

namespace InternetMarket.PaymentService.Application.Abstractions.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
        Task UpdateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
        Task DeleteAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
    }
}