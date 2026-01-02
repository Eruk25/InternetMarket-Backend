using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.UserService.Infrastructure.Persistence.DB.Configurations
{
    public class ResetPasswordTokenConfiguration : IEntityTypeConfiguration<ResetPasswordToken>
    {
        public void Configure(EntityTypeBuilder<ResetPasswordToken> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);
        }
    }
}