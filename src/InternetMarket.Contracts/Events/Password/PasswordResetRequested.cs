using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events.Password
{
    public record PasswordResetRequested
    {
        public string Email { get; init; }
        public string ResetLink { get; init; }

        public PasswordResetRequested()
        {
            Email = string.Empty;
            ResetLink = string.Empty;
        }
    }
}