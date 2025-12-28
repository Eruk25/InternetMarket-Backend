using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.API.DTOs.Requests.UpdateProfile
{
    public record UpdateUserProfileRequest(string? Name, string? Email);
}