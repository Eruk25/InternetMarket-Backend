using System.Data;
using System.Runtime.Serialization;

namespace InternetMarket.UserService.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
    public bool IsConfirmed { get; private set; }

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Role = "Role";
        IsConfirmed = false;
    }

    public void UpdateName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
    }

    public void UpdateEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
    }

    public void UpdatePassword(string password)
    {
        if (!string.IsNullOrWhiteSpace(password))
            Password = password;
    }
}
