using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Abstractions.Repositories
{
    public interface IResetPasswordTokenRepository
    {
        public Task<Domain.Entities.ResetPasswordToken?> GetByIdAsync(Guid Id);
        public Task CreateAsync(Domain.Entities.ResetPasswordToken token);
        public Task DeleteAsync(Domain.Entities.ResetPasswordToken token);
    }
}