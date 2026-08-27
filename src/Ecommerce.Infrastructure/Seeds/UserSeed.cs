using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities.User;

namespace Ecommerce.Infrastructure.Seeds;

public static class UserSeed
{
    public static async Task SeedUsersAsync(Data.EcommerceDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var users = new List<ApplicationUser>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@ecommerce.com",
                UserName = "admin@ecommerce.com",
                PhoneNumber = "+1234567890",
                IsAdmin = true,
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                UserName = "john.doe@example.com",
                PhoneNumber = "+1987654321",
                IsAdmin = false,
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                UserName = "jane.smith@example.com",
                PhoneNumber = "+1555123456",
                IsAdmin = false,
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-60)
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                UserName = "bob.johnson@example.com",
                PhoneNumber = "+1555987654",
                IsAdmin = false,
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Williams",
                Email = "alice.williams@example.com",
                UserName = "alice.williams@example.com",
                PhoneNumber = "+1555456789",
                IsAdmin = false,
                IsActive = false,
                EmailConfirmed = false,
                CreatedAt = DateTime.UtcNow.AddDays(-120)
            }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}
