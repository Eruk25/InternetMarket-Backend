using System.Data;
using System.Globalization;
using System.Runtime.Serialization;
using InternetMarket.UserService.Domain.ValueObjects;

namespace InternetMarket.UserService.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsConfirmed { get; private set; }

    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = UserRole.Client;
        IsConfirmed = false;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Name cannot be null or empty", nameof(name));
        Name = name;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void UpdatePassword(Password password)
    {
        Password = password;
    }

    public void UpdateRole(UserRole role)
    {
        Role = role;
    }

    public void UpdateConfirm(bool confirmed)
    {
        IsConfirmed = confirmed;
    }
}
