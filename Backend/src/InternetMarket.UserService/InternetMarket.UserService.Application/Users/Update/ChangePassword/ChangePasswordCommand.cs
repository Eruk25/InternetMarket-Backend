using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : IRequest;
}
