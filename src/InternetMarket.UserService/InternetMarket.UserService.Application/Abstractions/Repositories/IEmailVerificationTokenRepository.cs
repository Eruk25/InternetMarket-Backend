using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Abstractions.Repositories
{
    public interface IEmailVerificationTokenRepository
    {
        public Task<Domain.Entities.EmailVerificationToken> GetByIdAsync(Guid Id);
        public Task CreateAsync(Domain.Entities.EmailVerificationToken token);
        public Task DeleteAsync(Domain.Entities.EmailVerificationToken token);
    }
}