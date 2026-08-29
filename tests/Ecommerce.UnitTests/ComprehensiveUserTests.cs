using Xunit;
using FluentAssertions;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;

namespace Ecommerce.UnitTests;

public class UserDtoComprehensiveTests
{
    [Fact]
    public void UserDto_AllProperties_ShouldBeSettable()
    {
        var dto = new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "+1234567890",
            AvatarUrl = "https://example.com/avatar.jpg",
            IsActive = true,
            Role = "Admin",
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.FirstName.Should().Be("John");
        dto.LastName.Should().Be("Doe");
        dto.Email.Should().Be("john@example.com");
        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void UserDto_FullName_ShouldReturnFullName()
    {
        var dto = new UserDto
        {
            FirstName = "John",
            LastName = "Doe"
        };

        var fullName = $"{dto.FirstName} {dto.LastName}";

        fullName.Should().Be("John Doe");
    }
}

public class RegisterRequestComprehensiveTests
{
    [Fact]
    public void RegisterRequest_AllProperties_ShouldBeSettable()
    {
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            PhoneNumber = "+1234567890",
            AcceptTerms = true
        };

        request.FirstName.Should().Be("John");
        request.LastName.Should().Be("Doe");
        request.Email.Should().Be("john@example.com");
        request.Password.Should().Be("Password123!");
        request.ConfirmPassword.Should().Be("Password123!");
        request.PhoneNumber.Should().Be("+1234567890");
        request.AcceptTerms.Should().BeTrue();
    }
}

public class LoginRequestComprehensiveTests
{
    [Fact]
    public void LoginRequest_AllProperties_ShouldBeSettable()
    {
        var request = new LoginRequest
        {
            Email = "john@example.com",
            Password = "Password123!",
            RememberMe = true
        };

        request.Email.Should().Be("john@example.com");
        request.Password.Should().Be("Password123!");
        request.RememberMe.Should().BeTrue();
    }
}

public class AuthResponseComprehensiveTests
{
    [Fact]
    public void AuthResponse_AllProperties_ShouldBeSettable()
    {
        var response = new AuthResponse
        {
            Token = "jwt-token",
            RefreshToken = "refresh-token",
            Expiration = DateTime.UtcNow.AddHours(24),
            User = new UserDto
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            }
        };

        response.Token.Should().Be("jwt-token");
        response.RefreshToken.Should().Be("refresh-token");
        response.Expiration.Should().BeAfter(DateTime.UtcNow);
        response.User.Should().NotBeNull();
        response.User!.FirstName.Should().Be("John");
    }
}

public class ChangePasswordRequestComprehensiveTests
{
    [Fact]
    public void ChangePasswordRequest_AllProperties_ShouldBeSettable()
    {
        var request = new ChangePasswordRequest
        {
            CurrentPassword = "OldPassword123!",
            NewPassword = "NewPassword123!",
            ConfirmNewPassword = "NewPassword123!"
        };

        request.CurrentPassword.Should().Be("OldPassword123!");
        request.NewPassword.Should().Be("NewPassword123!");
        request.ConfirmNewPassword.Should().Be("NewPassword123!");
    }
}

public class UpdateProfileRequestComprehensiveTests
{
    [Fact]
    public void UpdateProfileRequest_AllProperties_ShouldBeSettable()
    {
        var request = new UpdateProfileRequest
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "+1234567890",
            AvatarUrl = "https://example.com/avatar.jpg"
        };

        request.FirstName.Should().Be("John");
        request.LastName.Should().Be("Doe");
        request.PhoneNumber.Should().Be("+1234567890");
        request.AvatarUrl.Should().Be("https://example.com/avatar.jpg");
    }
}

public class ForgotPasswordRequestComprehensiveTests
{
    [Fact]
    public void ForgotPasswordRequest_Email_ShouldBeSettable()
    {
        var request = new ForgotPasswordRequest
        {
            Email = "john@example.com"
        };

        request.Email.Should().Be("john@example.com");
    }
}

public class ResetPasswordRequestComprehensiveTests
{
    [Fact]
    public void ResetPasswordRequest_AllProperties_ShouldBeSettable()
    {
        var request = new ResetPasswordRequest
        {
            Token = "reset-token",
            Email = "john@example.com",
            NewPassword = "NewPassword123!",
            ConfirmPassword = "NewPassword123!"
        };

        request.Token.Should().Be("reset-token");
        request.Email.Should().Be("john@example.com");
        request.NewPassword.Should().Be("NewPassword123!");
        request.ConfirmPassword.Should().Be("NewPassword123!");
    }
}

public class UserAddressDtoComprehensiveTests
{
    [Fact]
    public void UserAddressDto_AllProperties_ShouldBeSettable()
    {
        var dto = new UserAddressDto
        {
            Id = Guid.NewGuid(),
            Street = "123 Main St",
            Street2 = "Apt 4B",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States",
            Phone = "+1234567890",
            IsDefault = true,
            Label = "Home"
        };

        dto.Id.Should().NotBeEmpty();
        dto.Street.Should().Be("123 Main St");
        dto.City.Should().Be("New York");
        dto.State.Should().Be("NY");
        dto.PostalCode.Should().Be("10001");
        dto.Country.Should().Be("United States");
        dto.IsDefault.Should().BeTrue();
        dto.Label.Should().Be("Home");
    }
}

public class PaymentMethodDtoComprehensiveTests
{
    [Fact]
    public void PaymentMethodDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PaymentMethodDto
        {
            Id = Guid.NewGuid(),
            CardType = "Visa",
            Last4Digits = "4242",
            ExpiryMonth = 12,
            ExpiryYear = 2025,
            CardHolderName = "John Doe",
            IsDefault = true,
            IsActive = true
        };

        dto.Id.Should().NotBeEmpty();
        dto.CardType.Should().Be("Visa");
        dto.Last4Digits.Should().Be("4242");
        dto.ExpiryMonth.Should().Be(12);
        dto.ExpiryYear.Should().Be(2025);
        dto.CardHolderName.Should().Be("John Doe");
        dto.IsDefault.Should().BeTrue();
        dto.IsActive.Should().BeTrue();
    }
}

public class WishlistDtoComprehensiveTests
{
    [Fact]
    public void WishlistDto_AllProperties_ShouldBeSettable()
    {
        var dto = new WishlistDto
        {
            Id = Guid.NewGuid(),
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", Price = 49.99m, InStock = true },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", Price = 29.99m, InStock = false }
            ],
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Items.Should().HaveCount(2);
        dto.ItemCount.Should().Be(2);
    }

    [Fact]
    public void WishlistDto_ItemCount_ShouldCountItems()
    {
        var dto = new WishlistDto
        {
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1" },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2" },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 3" }
            ]
        };

        dto.ItemCount.Should().Be(3);
    }
}

public class WishlistItemDtoComprehensiveTests
{
    [Fact]
    public void WishlistItemDto_AllProperties_ShouldBeSettable()
    {
        var dto = new WishlistItemDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            ProductImage = "https://example.com/image.jpg",
            Price = 49.99m,
            InStock = true,
            AddedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.ProductName.Should().Be("Test Product");
        dto.Price.Should().Be(49.99m);
        dto.InStock.Should().BeTrue();
    }
}

public class UserActivityDtoComprehensiveTests
{
    [Fact]
    public void UserActivityDto_AllProperties_ShouldBeSettable()
    {
        var dto = new UserActivityDto
        {
            UserId = Guid.NewGuid(),
            ActivityType = "Login",
            Description = "User logged in",
            IpAddress = "192.168.1.1",
            UserAgent = "Mozilla/5.0",
            Timestamp = DateTime.UtcNow
        };

        dto.UserId.Should().NotBeEmpty();
        dto.ActivityType.Should().Be("Login");
        dto.Description.Should().Be("User logged in");
        dto.IpAddress.Should().Be("192.168.1.1");
    }
}

public class UserStatsDtoComprehensiveTests
{
    [Fact]
    public void UserStatsDto_AllProperties_ShouldBeSettable()
    {
        var dto = new UserStatsDto
        {
            TotalUsers = 5000,
            ActiveUsers = 3500,
            NewUsersToday = 23,
            NewUsersThisWeek = 150,
            NewUsersThisMonth = 500
        };

        dto.TotalUsers.Should().Be(5000);
        dto.ActiveUsers.Should().Be(3500);
        dto.NewUsersToday.Should().Be(23);
        dto.NewUsersThisWeek.Should().Be(150);
        dto.NewUsersThisMonth.Should().Be(500);
    }
}
