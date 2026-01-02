using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Domain.Entities;
using InternetMarket.UserService.Infrastructure.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.UserService.Infrastructure.Persistence.Repositories
{
    public class ResetPasswordTokenRepository : IResetPasswordTokenRepository
    {
        private readonly UserContext _context;

        public ResetPasswordTokenRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<ResetPasswordToken?> GetByIdAsync(Guid Id)
        {
            var token = await _context.ResetPasswordTokens
                .FirstOrDefaultAsync(t => t.Id == Id);
            return token;
        }

        public async Task CreateAsync(ResetPasswordToken token)
        {
            await _context.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ResetPasswordToken token)
        {
            _context.Remove(token);
            await _context.SaveChangesAsync();
        }

    }
}