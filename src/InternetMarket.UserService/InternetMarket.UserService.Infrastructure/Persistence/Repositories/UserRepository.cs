using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Domain.Entities;
using InternetMarket.UserService.Infrastructure.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.UserService.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetByIdAsync(Guid Id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }
        public async Task<User?> GetByEmailAsync(string Email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.Value == Email);
            return user;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}