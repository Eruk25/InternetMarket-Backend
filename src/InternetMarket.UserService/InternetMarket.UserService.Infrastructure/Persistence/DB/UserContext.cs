using InternetMarket.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.UserService.Infrastructure.Persistence.DB
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
        public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}