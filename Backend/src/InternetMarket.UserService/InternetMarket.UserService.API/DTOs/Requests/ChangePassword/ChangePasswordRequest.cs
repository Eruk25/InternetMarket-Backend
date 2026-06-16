namespace InternetMarket.UserService.API.DTOs.Requests.ChangePassword
{
    public record ChangePasswordRequest(string OldPassword, string NewPassword);
}
