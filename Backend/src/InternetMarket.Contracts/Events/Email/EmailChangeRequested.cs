using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events.Email
{
    public record EmailChangeRequested(string Email, string VerificationLink);
}