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

public class InventoryDtoComprehensiveTests
{
    [Fact]
    public void WarehouseDto_AllProperties_ShouldBeSettable()
    {
        var dto = new WarehouseDto
        {
            Id = Guid.NewGuid(),
            Name = "Main Warehouse",
            Code = "WH-001",
            Address = "123 Industrial Ave",
            City = "Chicago",
            State = "IL",
            Country = "United States",
            PostalCode = "60601",
            Phone = "+1234567890",
            Email = "warehouse@example.com",
            Manager = "Jane Smith",
            Capacity = 10000,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Main Warehouse");
        dto.Code.Should().Be("WH-001");
        dto.Capacity.Should().Be(10000);
        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void WarehouseDto_OccupancyPercentage_ShouldCalculateCorrectly()
    {
        var dto = new WarehouseDto
        {
            Capacity = 1000,
            CurrentOccupancy = 750
        };

        dto.OccupancyPercentage.Should().Be(75m);
    }

    [Fact]
    public void WarehouseDto_OccupancyPercentage_ShouldReturnZeroWhenNoCapacity()
    {
        var dto = new WarehouseDto
        {
            Capacity = 0,
            CurrentOccupancy = 0
        };

        dto.OccupancyPercentage.Should().Be(0);
    }

    [Fact]
    public void WarehouseDto_AvailableCapacity_ShouldCalculateCorrectly()
    {
        var dto = new WarehouseDto
        {
            Capacity = 1000,
            CurrentOccupancy = 750
        };

        dto.AvailableCapacity.Should().Be(250);
    }
}

public class WarehouseInventoryDtoComprehensiveTests
{
    [Fact]
    public void WarehouseInventoryDto_AllProperties_ShouldBeSettable()
    {
        var dto = new WarehouseInventoryDto
        {
            Id = Guid.NewGuid(),
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            Quantity = 100,
            ReservedQuantity = 20,
            ReorderPoint = 30,
            SafetyStock = 10,
            LastRestockedAt = DateTime.UtcNow.AddDays(-7),
            LastCountedAt = DateTime.UtcNow.AddDays(-30),
            CostPerUnit = 25.50m,
            Location = "Aisle 5, Shelf 3"
        };

        dto.Id.Should().NotBeEmpty();
        dto.Quantity.Should().Be(100);
        dto.ReservedQuantity.Should().Be(20);
        dto.AvailableQuantity.Should().Be(80);
        dto.IsLowStock.Should().BeFalse();
        dto.IsOutOfStock.Should().BeFalse();
    }

    [Fact]
    public void WarehouseInventoryDto_IsLowStock_ShouldReturnTrueWhenBelowReorderPoint()
    {
        var dto = new WarehouseInventoryDto
        {
            Quantity = 25,
            ReservedQuantity = 0,
            ReorderPoint = 30
        };

        dto.IsLowStock.Should().BeTrue();
    }

    [Fact]
    public void WarehouseInventoryDto_IsOutOfStock_ShouldReturnTrueWhenZeroQuantity()
    {
        var dto = new WarehouseInventoryDto
        {
            Quantity = 0,
            ReservedQuantity = 0
        };

        dto.IsOutOfStock.Should().BeTrue();
    }

    [Fact]
    public void WarehouseInventoryDto_NeedsReorder_ShouldReturnTrueWhenAtOrBelowReorderPoint()
    {
        var dto = new WarehouseInventoryDto
        {
            Quantity = 30,
            ReservedQuantity = 0,
            ReorderPoint = 30
        };

        dto.NeedsReorder.Should().BeTrue();
    }

    [Fact]
    public void WarehouseInventoryDto_TotalValue_ShouldCalculateCorrectly()
    {
        var dto = new WarehouseInventoryDto
        {
            Quantity = 100,
            CostPerUnit = 25.50m
        };

        dto.TotalValue.Should().Be(2550m);
    }
}

public class StockMovementDtoComprehensiveTests
{
    [Fact]
    public void StockMovementDto_AllProperties_ShouldBeSettable()
    {
        var dto = new StockMovementDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            MovementType = "Inbound",
            Quantity = 50,
            ReferenceNumber = "PO-001",
            Notes = "Purchase order received",
            PerformedBy = "admin@example.com",
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.MovementType.Should().Be("Inbound");
        dto.Quantity.Should().Be(50);
        dto.ReferenceNumber.Should().Be("PO-001");
    }
}

public class SupplierDtoComprehensiveTests
{
    [Fact]
    public void SupplierDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SupplierDto
        {
            Id = Guid.NewGuid(),
            Name = "Tech Supplies Inc",
            ContactPerson = "John Smith",
            Email = "contact@techsupplies.com",
            Phone = "+1234567890",
            Address = "456 Supply Chain Rd",
            City = "San Francisco",
            State = "CA",
            Country = "United States",
            PostalCode = "94101",
            Website = "https://techsupplies.com",
            Rating = 4.5,
            LeadTimeDays = 7,
            PaymentTerms = "Net 30",
            IsActive = true,
            ProductsSupplied = 25,
            TotalOrders = 150,
            TotalOrderValue = 50000m,
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Tech Supplies Inc");
        dto.Rating.Should().Be(4.5);
        dto.LeadTimeDays.Should().Be(7);
        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void SupplierDto_AverageOrderValue_ShouldCalculateCorrectly()
    {
        var dto = new SupplierDto
        {
            TotalOrders = 100,
            TotalOrderValue = 50000m
        };

        dto.AverageOrderValue.Should().Be(500m);
    }

    [Fact]
    public void SupplierDto_AverageOrderValue_ShouldReturnZeroWhenNoOrders()
    {
        var dto = new SupplierDto
        {
            TotalOrders = 0,
            TotalOrderValue = 0
        };

        dto.AverageOrderValue.Should().Be(0);
    }
}

public class PurchaseOrderDtoComprehensiveTests
{
    [Fact]
    public void PurchaseOrderDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PurchaseOrderDto
        {
            Id = Guid.NewGuid(),
            OrderNumber = "PO-20240101-001",
            SupplierId = Guid.NewGuid(),
            SupplierName = "Tech Supplies Inc",
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            Status = "Pending",
            SubTotal = 2500m,
            TaxAmount = 200m,
            ShippingCost = 150m,
            TotalAmount = 2850m,
            ExpectedDeliveryDate = DateTime.UtcNow.AddDays(14),
            Notes = "Urgent order",
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", Sku = "SKU-001", Quantity = 50, UnitPrice = 25m, TotalPrice = 1250m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", Sku = "SKU-002", Quantity = 50, UnitPrice = 25m, TotalPrice = 1250m }
            ],
            CreatedBy = "admin@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.OrderNumber.Should().Be("PO-20240101-001");
        dto.TotalAmount.Should().Be(2850m);
        dto.Items.Should().HaveCount(2);
    }

    [Fact]
    public void PurchaseOrderDto_IsOverdue_ShouldReturnTrueWhenPastExpectedDelivery()
    {
        var dto = new PurchaseOrderDto
        {
            ExpectedDeliveryDate = DateTime.UtcNow.AddDays(-1),
            Status = "Pending"
        };

        dto.IsOverdue.Should().BeTrue();
    }

    [Fact]
    public void PurchaseOrderDto_IsOverdue_ShouldReturnFalseWhenNotPastExpectedDelivery()
    {
        var dto = new PurchaseOrderDto
        {
            ExpectedDeliveryDate = DateTime.UtcNow.AddDays(14),
            Status = "Pending"
        };

        dto.IsOverdue.Should().BeFalse();
    }

    [Fact]
    public void PurchaseOrderDto_IsFullyReceived_ShouldReturnTrueWhenAllItemsReceived()
    {
        var dto = new PurchaseOrderDto
        {
            Items =
            [
                new() { Quantity = 50, ReceivedQuantity = 50 },
                new() { Quantity = 30, ReceivedQuantity = 30 }
            ]
        };

        dto.IsFullyReceived.Should().BeTrue();
    }

    [Fact]
    public void PurchaseOrderDto_IsFullyReceived_ShouldReturnFalseWhenNotAllItemsReceived()
    {
        var dto = new PurchaseOrderDto
        {
            Items =
            [
                new() { Quantity = 50, ReceivedQuantity = 40 },
                new() { Quantity = 30, ReceivedQuantity = 30 }
            ]
        };

        dto.IsFullyReceived.Should().BeFalse();
    }
}

public class InventoryAlertDtoComprehensiveTests
{
    [Fact]
    public void InventoryAlertDto_AllProperties_ShouldBeSettable()
    {
        var dto = new InventoryAlertDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            AlertType = "LowStock",
            CurrentQuantity = 5,
            Threshold = 10,
            Severity = "Warning",
            Message = "Stock is low",
            CreatedAt = DateTime.UtcNow,
            IsAcknowledged = false
        };

        dto.Id.Should().NotBeEmpty();
        dto.AlertType.Should().Be("LowStock");
        dto.CurrentQuantity.Should().Be(5);
        dto.Threshold.Should().Be(10);
        dto.Severity.Should().Be("Warning");
    }
}

public class InventoryForecastDtoComprehensiveTests
{
    [Fact]
    public void InventoryForecastDto_AllProperties_ShouldBeSettable()
    {
        var dto = new InventoryForecastDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            CurrentStock = 100,
            AverageDailySales = 5,
            DaysUntilStockout = 20,
            ReorderQuantity = 150,
            ReorderDate = DateTime.UtcNow.AddDays(18),
            ForecastedDemand = new List<InventoryForecastItem>
            {
                new() { Date = DateTime.UtcNow.AddDays(1), PredictedQuantity = 95 },
                new() { Date = DateTime.UtcNow.AddDays(2), PredictedQuantity = 90 },
                new() { Date = DateTime.UtcNow.AddDays(3), PredictedQuantity = 85 }
            }
        };

        dto.ProductId.Should().NotBeEmpty();
        dto.CurrentStock.Should().Be(100);
        dto.AverageDailySales.Should().Be(5);
        dto.DaysUntilStockout.Should().Be(20);
        dto.ForecastedDemand.Should().HaveCount(3);
    }
}

public class InventoryForecastItemComprehensiveTests
{
    [Fact]
    public void InventoryForecastItem_AllProperties_ShouldBeSettable()
    {
        var dto = new InventoryForecastItem
        {
            Date = DateTime.UtcNow,
            PredictedQuantity = 95
        };

        dto.Date.Should().Be(DateTime.UtcNow);
        dto.PredictedQuantity.Should().Be(95);
    }
}

public class CreateWarehouseRequestComprehensiveTests
{
    [Fact]
    public void CreateWarehouseRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateWarehouseRequest
        {
            Name = "New Warehouse",
            Code = "WH-NEW",
            Address = "789 Warehouse Blvd",
            City = "Dallas",
            State = "TX",
            Country = "United States",
            PostalCode = "75201",
            Phone = "+1234567890",
            Email = "newwarehouse@example.com",
            Manager = "John Doe",
            Capacity = 5000
        };

        request.Name.Should().Be("New Warehouse");
        request.Code.Should().Be("WH-NEW");
        request.Capacity.Should().Be(5000);
    }
}

public class AdjustStockRequestComprehensiveTests
{
    [Fact]
    public void AdjustStockRequest_AllProperties_ShouldBeSettable()
    {
        var request = new AdjustStockRequest
        {
            ProductId = Guid.NewGuid(),
            WarehouseId = Guid.NewGuid(),
            Adjustment = -10,
            Reason = "Damaged goods",
            ReferenceNumber = "ADJ-001"
        };

        request.ProductId.Should().NotBeEmpty();
        request.WarehouseId.Should().NotBeEmpty();
        request.Adjustment.Should().Be(-10);
        request.Reason.Should().Be("Damaged goods");
    }
}

public class TransferStockRequestComprehensiveTests
{
    [Fact]
    public void TransferStockRequest_AllProperties_ShouldBeSettable()
    {
        var request = new TransferStockRequest
        {
            ProductId = Guid.NewGuid(),
            FromWarehouseId = Guid.NewGuid(),
            ToWarehouseId = Guid.NewGuid(),
            Quantity = 50,
            Reason = "Restocking",
            ReferenceNumber = "TRF-001"
        };

        request.ProductId.Should().NotBeEmpty();
        request.FromWarehouseId.Should().NotBeEmpty();
        request.ToWarehouseId.Should().NotBeEmpty();
        request.Quantity.Should().Be(50);
        request.Reason.Should().Be("Restocking");
    }
}

public class InventoryAuditDtoComprehensiveTests
{
    [Fact]
    public void InventoryAuditDto_AllProperties_ShouldBeSettable()
    {
        var dto = new InventoryAuditDto
        {
            Id = Guid.NewGuid(),
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            ExpectedQuantity = 100,
            ActualQuantity = 95,
            Variance = -5,
            VariancePercentage = -5.0m,
            Notes = "Found 5 damaged units",
            PerformedBy = "admin@example.com",
            PerformedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.ExpectedQuantity.Should().Be(100);
        dto.ActualQuantity.Should().Be(95);
        dto.Variance.Should().Be(-5);
        dto.VariancePercentage.Should().Be(-5.0m);
    }

    [Fact]
    public void InventoryAuditDto_IsVariance_ShouldReturnTrueWhenVarianceNonZero()
    {
        var dto = new InventoryAuditDto
        {
            Variance = -5
        };

        dto.IsVariance.Should().BeTrue();
    }

    [Fact]
    public void InventoryAuditDto_IsVariance_ShouldReturnFalseWhenVarianceZero()
    {
        var dto = new InventoryAuditDto
        {
            Variance = 0
        };

        dto.IsVariance.Should().BeFalse();
    }
}
