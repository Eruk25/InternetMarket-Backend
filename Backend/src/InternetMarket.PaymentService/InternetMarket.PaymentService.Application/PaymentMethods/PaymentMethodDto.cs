using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Application.PaymentMethods
{
    public record PaymentMethodDto(string Name, string SystemName, bool IsActive);
}