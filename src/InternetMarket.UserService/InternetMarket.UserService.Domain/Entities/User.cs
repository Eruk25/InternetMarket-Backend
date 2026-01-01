using System.Data;
using System.Runtime.Serialization;

namespace InternetMarket.UserService.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Guid CartId { get; private set; }
    public string Role { get; private set; }
    public bool IsConfirmed { get; private set; }

    public User(string name, string email, string password, Guid cartId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        CartId = cartId;
        Role = "Role";
        IsConfirmed = false;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Name cannot be null or empty", nameof(name));
        Name = name;
    }

    public void UpdateEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException("Email cannot be null or empty", nameof(email));
        Email = email;
    }

    public void UpdatePassword(string password)
    {
        if (!string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException("Password cannot be null or empty", nameof(password));
        Password = password;
    }

    public void UpdateRole(string role)
    {
        if (!string.IsNullOrWhiteSpace(role))
            throw new ArgumentNullException("Role cannot be null or empty", nameof(role));
        Role = role;
    }
}
