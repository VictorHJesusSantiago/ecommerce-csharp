using Ecommerce.Domain.Entities.User;

namespace Ecommerce.Infrastructure.Seeds;

public static class PaymentMethodSeed
{
    public static List<PaymentMethod> GetPaymentMethods(Guid userId)
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = "CreditCard",
                LastFourDigits = "4242",
                CardType = "Visa",
                ExpiryMonth = 12,
                ExpiryYear = 2027,
                HolderName = "John Doe",
                IsDefault = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = "CreditCard",
                LastFourDigits = "1234",
                CardType = "Mastercard",
                ExpiryMonth = 6,
                ExpiryYear = 2028,
                HolderName = "John Doe",
                IsDefault = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-60)
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = "PayPal",
                PayPalEmail = "john.doe@example.com",
                IsDefault = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            }
        ];
    }
}

public static class AddressSeed
{
    public static List<Address> GetAddresses(Guid userId)
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FirstName = "John",
                LastName = "Doe",
                AddressLine1 = "123 Main Street",
                AddressLine2 = "Apt 4B",
                City = "New York",
                State = "NY",
                PostalCode = "10001",
                Country = "US",
                PhoneNumber = "+12125551234",
                IsDefault = true,
                Type = "Home"
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FirstName = "John",
                LastName = "Doe",
                AddressLine1 = "456 Work Avenue",
                City = "New York",
                State = "NY",
                PostalCode = "10002",
                Country = "US",
                PhoneNumber = "+12125555678",
                IsDefault = false,
                Type = "Work"
            }
        ];
    }
}
