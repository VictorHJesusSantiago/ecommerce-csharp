using Ecommerce.Domain.Entities.Inventory;

namespace Ecommerce.Infrastructure.Seeds;

public static class WarehouseSeed
{
    public static List<Warehouse> GetWarehouses()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Main Distribution Center",
                Code = "MDC-001",
                Address = "123 Commerce Blvd",
                City = "New York",
                State = "NY",
                Country = "USA",
                PostalCode = "10001",
                Phone = "+12125551000",
                Email = "warehouse@ecommerce.com",
                ManagerName = "John Manager",
                Capacity = 100000,
                CurrentOccupancy = 65000,
                IsActive = true,
                IsDefault = true,
                SupportsPickup = true,
                SupportsShipping = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "West Coast Hub",
                Code = "WCH-002",
                Address = "456 Pacific Ave",
                City = "Los Angeles",
                State = "CA",
                Country = "USA",
                PostalCode = "90001",
                Phone = "+13105552000",
                Email = "west@ecommerce.com",
                ManagerName = "Jane Supervisor",
                Capacity = 75000,
                CurrentOccupancy = 40000,
                IsActive = true,
                IsDefault = false,
                SupportsPickup = true,
                SupportsShipping = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Midwest Fulfillment",
                Code = "MFW-003",
                Address = "789 Lake Shore Dr",
                City = "Chicago",
                State = "IL",
                Country = "USA",
                PostalCode = "60601",
                Phone = "+13125553000",
                Email = "midwest@ecommerce.com",
                ManagerName = "Bob Warehouse",
                Capacity = 50000,
                CurrentOccupancy = 30000,
                IsActive = true,
                IsDefault = false,
                SupportsPickup = false,
                SupportsShipping = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "European Distribution",
                Code = "EDC-004",
                Address = "321 Industrial Park",
                City = "London",
                State = "",
                Country = "UK",
                PostalCode = "EC1A 1BB",
                Phone = "+442075554000",
                Email = "eu@ecommerce.com",
                ManagerName = "EU Manager",
                Capacity = 60000,
                CurrentOccupancy = 20000,
                IsActive = true,
                IsDefault = false,
                SupportsPickup = true,
                SupportsShipping = true
            }
        ];
    }
}
