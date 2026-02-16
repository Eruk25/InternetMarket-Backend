using InternetMarket.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.UserService.Infrastructure.Persistence.DB.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();
            builder.Property(u => u.Password)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(u => u.Role)
                .IsRequired();
            builder.Property(u => u.IsConfirmed)
                .IsRequired();
        }
    }
}