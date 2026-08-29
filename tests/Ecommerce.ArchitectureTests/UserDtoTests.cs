using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.User;

namespace Ecommerce.ArchitectureTests;

public class UserDtoTests
{
    [Fact]
    public void UserDto_ShouldHaveRequiredProperties()
    {
        var dto = new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "+1234567890",
            ProfileImageUrl = "/images/profile.jpg",
            IsEmailVerified = true,
            IsPhoneVerified = false,
            TwoFactorEnabled = false,
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
            Roles = new List<string> { "Customer", "Admin" },
            AddressCount = 3,
            OrderCount = 15,
            TotalSpent = 2500m,
            IsActive = true
        };

        dto.FullName.Should().Be("John Doe");
        dto.Roles.Should().Contain("Admin");
        dto.TotalSpent.Should().Be(2500m);
    }

    [Fact]
    public void RegisterUserDto_ShouldHaveRequiredProperties()
    {
        var dto = new RegisterUserDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane@example.com",
            Password = "P@ssw0rd123!",
            ConfirmPassword = "P@ssw0rd123!",
            PhoneNumber = "+9876543210"
        };

        dto.FullName.Should().Be("Jane Smith");
    }

    [Fact]
    public void LoginDto_ShouldHaveRequiredProperties()
    {
        var dto = new LoginDto
        {
            Email = "user@example.com",
            Password = "password123",
            RememberMe = true
        };

        dto.Email.Should().Be("user@example.com");
        dto.RememberMe.Should().BeTrue();
    }

    [Fact]
    public void UserProfileDto_ShouldHaveRequiredProperties()
    {
        var dto = new UserProfileDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "+1234567890",
            ProfileImageUrl = "/images/profile.jpg",
            DateOfBirth = new DateTime(1990, 1, 15),
            Gender = "Male",
            AboutMe = "Hello, I'm John!",
            PreferredLanguage = "en",
            PreferredCurrency = "USD",
            NewsletterSubscribed = true,
            Addresses = new List<UserAddressDto>(),
            PaymentMethods = new List<UserPaymentMethodDto>()
        };

        dto.PreferredLanguage.Should().Be("en");
        dto.NewsletterSubscribed.Should().BeTrue();
    }

    [Fact]
    public void UserAddressDto_ShouldHaveRequiredProperties()
    {
        var dto = new UserAddressDto
        {
            Id = Guid.NewGuid(),
            Label = "Home",
            FullName = "John Doe",
            Street = "123 Main St",
            Street2 = "Apt 4B",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "US",
            Phone = "+1234567890",
            IsDefault = true
        };

        dto.IsDefault.Should().BeTrue();
        dto.FullAddress.Should().Contain("New York");
    }

    [Fact]
    public void UserPaymentMethodDto_ShouldHaveRequiredProperties()
    {
        var dto = new UserPaymentMethodDto
        {
            Id = Guid.NewGuid(),
            CardType = "Visa",
            LastFourDigits = "4242",
            ExpiryMonth = 12,
            ExpiryYear = 2027,
            CardholderName = "John Doe",
            IsDefault = true
        };

        dto.DisplayName.Should().Contain("Visa");
        dto.DisplayName.Should().Contain("4242");
    }

    [Fact]
    public void ChangePasswordDto_ShouldHaveRequiredProperties()
    {
        var dto = new ChangePasswordDto
        {
            CurrentPassword = "oldpass",
            NewPassword = "newpass123",
            ConfirmNewPassword = "newpass123"
        };

        dto.CurrentPassword.Should().Be("oldpass");
    }

    [Fact]
    public void UserActivityDto_ShouldHaveRequiredProperties()
    {
        var dto = new UserActivityDto
        {
            UserId = Guid.NewGuid(),
            Action = "Login",
            Details = "Logged in from Chrome on Windows",
            IpAddress = "192.168.1.1",
            UserAgent = "Mozilla/5.0",
            Timestamp = DateTime.UtcNow
        };

        dto.Action.Should().Be("Login");
    }
}
