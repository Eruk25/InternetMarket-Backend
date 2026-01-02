using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Domain.Entities
{
    public class ResetPasswordToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}