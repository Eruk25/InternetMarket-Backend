using InternetMarket.UserService.Domain.Entities;
using InternetMarket.UserService.Domain.ValueObjects;

namespace InternetMarket.UserService.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(Guid Id);
        public Task<User?> GetByEmailAsync(Email email);
        public Task CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}