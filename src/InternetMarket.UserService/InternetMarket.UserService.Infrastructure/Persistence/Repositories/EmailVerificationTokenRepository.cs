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
    public class EmailVerificationTokenRepository : IEmailVerificationTokenRepository
    {
        private readonly UserContext _context;

        public EmailVerificationTokenRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<EmailVerificationToken?> GetByIdAsync(Guid Id)
        {
            var token = await _context.Tokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == Id);
            return token;
        }

        public async Task DeleteAsync(EmailVerificationToken token)
        {
            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(EmailVerificationToken token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
    }
}