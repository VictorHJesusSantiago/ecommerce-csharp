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

public class DashboardDtoComprehensiveTests
{
    [Fact]
    public void DashboardDto_AllProperties_ShouldBeSettable()
    {
        var dto = new DashboardDto
        {
            TotalRevenue = 150000m,
            TotalOrders = 1500,
            TotalProducts = 250,
            TotalCustomers = 3000,
            RevenueGrowth = 15.5m,
            OrderGrowth = 12.3m,
            AverageOrderValue = 100m,
            ConversionRate = 3.2m,
            RecentOrders =
            [
                new() { OrderNumber = "ORD-001", TotalAmount = 150m, Status = "Processing", CreatedAt = DateTime.UtcNow },
                new() { OrderNumber = "ORD-002", TotalAmount = 250m, Status = "Shipped", CreatedAt = DateTime.UtcNow }
            ],
            TopProducts =
            [
                new() { ProductName = "Product 1", TotalSales = 500, Revenue = 25000m },
                new() { ProductName = "Product 2", TotalSales = 400, Revenue = 20000m }
            ],
            LowStockProducts =
            [
                new() { ProductName = "Product 3", StockQuantity = 5, Sku = "SKU-003" },
                new() { ProductName = "Product 4", StockQuantity = 3, Sku = "SKU-004" }
            ],
            SalesByDay =
            [
                new() { Date = DateTime.UtcNow.AddDays(-1), Revenue = 5000m, OrderCount = 50 },
                new() { Date = DateTime.UtcNow, Revenue = 6000m, OrderCount = 60 }
            ],
            SalesByCategory =
            [
                new() { CategoryName = "Electronics", Revenue = 80000m, Percentage = 53.3m },
                new() { CategoryName = "Clothing", Revenue = 70000m, Percentage = 46.7m }
            ],
            RevenueByPaymentMethod =
            [
                new() { PaymentMethod = "Credit Card", Amount = 100000m, Percentage = 66.7m },
                new() { PaymentMethod = "PayPal", Amount = 50000m, Percentage = 33.3m }
            ],
            OrderStatusBreakdown =
            [
                new() { Status = "Pending", Count = 100, Percentage = 6.7m },
                new() { Status = "Processing", Count = 500, Percentage = 33.3m },
                new() { Status = "Shipped", Count = 400, Percentage = 26.7m },
                new() { Status = "Delivered", Count = 500, Percentage = 33.3m }
            ],
            InventorySummary = new InventorySummaryDto
            {
                TotalProducts = 250,
                InStock = 200,
                LowStock = 30,
                OutOfStock = 20,
                TotalValue = 500000m
            },
            MarketingSummary = new MarketingSummaryDto
            {
                ActiveCoupons = 5,
                ActivePromotions = 3,
                ActiveBanners = 4,
                NewsletterSubscribers = 1500
            },
            Period = "month"
        };

        dto.TotalRevenue.Should().Be(150000m);
        dto.TotalOrders.Should().Be(1500);
        dto.RecentOrders.Should().HaveCount(2);
        dto.TopProducts.Should().HaveCount(2);
        dto.LowStockProducts.Should().HaveCount(2);
        dto.SalesByDay.Should().HaveCount(2);
        dto.SalesByCategory.Should().HaveCount(2);
        dto.RevenueByPaymentMethod.Should().HaveCount(2);
        dto.OrderStatusBreakdown.Should().HaveCount(4);
    }

    [Fact]
    public void DashboardDto_RevenuePerCustomer_ShouldCalculateCorrectly()
    {
        var dto = new DashboardDto
        {
            TotalRevenue = 150000m,
            TotalCustomers = 3000
        };

        dto.RevenuePerCustomer.Should().Be(50m);
    }

    [Fact]
    public void DashboardDto_RevenuePerCustomer_ShouldReturnZeroWhenNoCustomers()
    {
        var dto = new DashboardDto
        {
            TotalRevenue = 150000m,
            TotalCustomers = 0
        };

        dto.RevenuePerCustomer.Should().Be(0);
    }

    [Fact]
    public void DashboardDto_OrdersPerCustomer_ShouldCalculateCorrectly()
    {
        var dto = new DashboardDto
        {
            TotalOrders = 1500,
            TotalCustomers = 3000
        };

        dto.OrdersPerCustomer.Should().Be(0.5m);
    }
}

public class RevenueReportDtoComprehensiveTests
{
    [Fact]
    public void RevenueReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new RevenueReportDto
        {
            TotalRevenue = 150000m,
            TotalCost = 90000m,
            GrossProfit = 60000m,
            GrossMargin = 40m,
            NetProfit = 45000m,
            NetMargin = 30m,
            TotalTaxes = 12000m,
            TotalShipping = 5000m,
            TotalDiscounts = 8000m,
            AverageOrderValue = 100m,
            AverageOrderMargin = 40m,
            Period = "month",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            DailyRevenue =
            [
                new() { Date = DateTime.UtcNow.AddDays(-1), Revenue = 5000m, Cost = 3000m, Profit = 2000m, OrderCount = 50 },
                new() { Date = DateTime.UtcNow, Revenue = 6000m, Cost = 3600m, Profit = 2400m, OrderCount = 60 }
            ],
            RevenueByCategory =
            [
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Electronics", Revenue = 80000m, Percentage = 53.3m },
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Clothing", Revenue = 70000m, Percentage = 46.7m }
            ],
            TopProducts =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", Revenue = 25000m, QuantitySold = 500, Margin = 40m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", Revenue = 20000m, QuantitySold = 400, Margin = 35m }
            ]
        };

        dto.TotalRevenue.Should().Be(150000m);
        dto.GrossProfit.Should().Be(60000m);
        dto.GrossMargin.Should().Be(40m);
        dto.DailyRevenue.Should().HaveCount(2);
        dto.RevenueByCategory.Should().HaveCount(2);
        dto.TopProducts.Should().HaveCount(2);
    }
}

public class SalesReportDtoComprehensiveTests
{
    [Fact]
    public void SalesReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SalesReportDto
        {
            TotalSales = 1500,
            TotalRevenue = 150000m,
            TotalItemsSold = 4500,
            AverageItemsPerOrder = 3m,
            AverageOrderValue = 100m,
            Period = "month",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            DailySales =
            [
                new() { Date = DateTime.UtcNow.AddDays(-1), OrderCount = 50, Revenue = 5000m, ItemsSold = 150 },
                new() { Date = DateTime.UtcNow, OrderCount = 60, Revenue = 6000m, ItemsSold = 180 }
            ],
            SalesByHour =
            [
                new() { Hour = 9, OrderCount = 10, Revenue = 1000m },
                new() { Hour = 12, OrderCount = 25, Revenue = 2500m },
                new() { Hour = 18, OrderCount = 30, Revenue = 3000m }
            ],
            SalesByDayOfWeek =
            [
                new() { Day = "Monday", OrderCount = 200, Revenue = 20000m },
                new() { Day = "Tuesday", OrderCount = 180, Revenue = 18000m },
                new() { Day = "Wednesday", OrderCount = 190, Revenue = 19000m },
                new() { Day = "Thursday", OrderCount = 170, Revenue = 17000m },
                new() { Day = "Friday", OrderCount = 210, Revenue = 21000m },
                new() { Day = "Saturday", OrderCount = 300, Revenue = 30000m },
                new() { Day = "Sunday", OrderCount = 250, Revenue = 25000m }
            ],
            TopSellingProducts =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", QuantitySold = 500, Revenue = 25000m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", QuantitySold = 400, Revenue = 20000m }
            ]
        };

        dto.TotalSales.Should().Be(1500);
        dto.TotalRevenue.Should().Be(150000m);
        dto.DailySales.Should().HaveCount(2);
        dto.SalesByHour.Should().HaveCount(3);
        dto.SalesByDayOfWeek.Should().HaveCount(7);
        dto.TopSellingProducts.Should().HaveCount(2);
    }
}

public class CustomerReportDtoComprehensiveTests
{
    [Fact]
    public void CustomerReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new CustomerReportDto
        {
            TotalCustomers = 3000,
            NewCustomers = 500,
            ReturningCustomers = 2500,
            CustomerRetentionRate = 83.3m,
            AverageCustomerLifetimeValue = 500m,
            AverageCustomerAge = 35,
            TopCountries =
            [
                new() { Country = "United States", CustomerCount = 1500, Percentage = 50m },
                new() { Country = "United Kingdom", CustomerCount = 600, Percentage = 20m },
                new() { Country = "Canada", CustomerCount = 300, Percentage = 10m }
            ],
            TopCities =
            [
                new() { City = "New York", CustomerCount = 500, Percentage = 16.7m },
                new() { City = "Los Angeles", CustomerCount = 400, Percentage = 13.3m },
                new() { City = "Chicago", CustomerCount = 300, Percentage = 10m }
            ],
            CustomerSegments =
            [
                new() { Segment = "VIP", Count = 100, Percentage = 3.3m, AverageSpend = 2000m },
                new() { Segment = "Regular", Count = 1500, Percentage = 50m, AverageSpend = 500m },
                new() { Segment = "New", Count = 500, Percentage = 16.7m, AverageSpend = 100m },
                new() { Segment = "At Risk", Count = 900, Percentage = 30m, AverageSpend = 200m }
            ],
            CustomerAcquisition =
            [
                new() { Date = DateTime.UtcNow.AddDays(-30), NewCustomers = 50 },
                new() { Date = DateTime.UtcNow.AddDays(-29), NewCustomers = 45 },
                new() { Date = DateTime.UtcNow.AddDays(-28), NewCustomers = 55 }
            ],
            TopCustomers =
            [
                new() { CustomerId = Guid.NewGuid(), CustomerName = "John Doe", TotalOrders = 50, TotalSpent = 5000m },
                new() { CustomerId = Guid.NewGuid(), CustomerName = "Jane Smith", TotalOrders = 40, TotalSpent = 4000m }
            ]
        };

        dto.TotalCustomers.Should().Be(3000);
        dto.CustomerRetentionRate.Should().Be(83.3m);
        dto.TopCountries.Should().HaveCount(3);
        dto.TopCities.Should().HaveCount(3);
        dto.CustomerSegments.Should().HaveCount(4);
        dto.CustomerAcquisition.Should().HaveCount(3);
        dto.TopCustomers.Should().HaveCount(2);
    }
}

public class InventoryReportDtoComprehensiveTests
{
    [Fact]
    public void InventoryReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new InventoryReportDto
        {
            TotalProducts = 250,
            InStock = 200,
            LowStock = 30,
            OutOfStock = 20,
            TotalInventoryValue = 500000m,
            AverageStockLevel = 100,
            StockTurnoverRate = 4.5m,
            DaysOfSupply = 81,
            Period = "month",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            TopProductsByValue =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", StockQuantity = 500, Value = 25000m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", StockQuantity = 400, Value = 20000m }
            ],
            TopProductsByQuantity =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 3", StockQuantity = 1000, Value = 10000m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 4", StockQuantity = 800, Value = 8000m }
            ],
            LowStockProducts =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 5", Sku = "SKU-005", CurrentStock = 5, ReorderPoint = 10, Warehouse = "Main" },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 6", Sku = "SKU-006", CurrentStock = 3, ReorderPoint = 5, Warehouse = "Main" }
            ],
            WarehouseBreakdown =
            [
                new() { WarehouseId = Guid.NewGuid(), WarehouseName = "Main", ProductCount = 200, TotalValue = 400000m },
                new() { WarehouseId = Guid.NewGuid(), WarehouseName = "Secondary", ProductCount = 100, TotalValue = 100000m }
            ],
            StockMovements =
            [
                new() { Date = DateTime.UtcNow.AddDays(-1), Inbound = 100, Outbound = 50, Adjustments = 5 },
                new() { Date = DateTime.UtcNow, Inbound = 120, Outbound = 60, Adjustments = 3 }
            ]
        };

        dto.TotalProducts.Should().Be(250);
        dto.InStock.Should().Be(200);
        dto.TotalInventoryValue.Should().Be(500000m);
        dto.TopProductsByValue.Should().HaveCount(2);
        dto.LowStockProducts.Should().HaveCount(2);
        dto.WarehouseBreakdown.Should().HaveCount(2);
        dto.StockMovements.Should().HaveCount(2);
    }
}

public class ProductPerformanceDtoComprehensiveTests
{
    [Fact]
    public void ProductPerformanceDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductPerformanceDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            TotalRevenue = 25000m,
            TotalQuantitySold = 500,
            AverageRating = 4.5,
            ReviewCount = 128,
            ViewCount = 10000,
            ConversionRate = 5.0m,
            ReturnRate = 2.0m,
            AverageOrderQuantity = 1.5m,
            RepeatPurchaseRate = 15.0m,
            StockTurnover = 4.5m,
            ProfitMargin = 35.0m
        };

        dto.ProductId.Should().NotBeEmpty();
        dto.ProductName.Should().Be("Test Product");
        dto.TotalRevenue.Should().Be(25000m);
        dto.TotalQuantitySold.Should().Be(500);
        dto.ConversionRate.Should().Be(5.0m);
    }
}

public class PaymentReportDtoComprehensiveTests
{
    [Fact]
    public void PaymentReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PaymentReportDto
        {
            TotalPayments = 1500,
            TotalRevenue = 150000m,
            TotalRefunds = 5000m,
            TotalChargebacks = 1000m,
            SuccessfulPayments = 1450,
            FailedPayments = 50,
            RefundRate = 3.3m,
            ChargebackRate = 0.7m,
            AveragePaymentAmount = 100m,
            Period = "month",
            PaymentsByMethod =
            [
                new() { Method = "Credit Card", Count = 1000, Revenue = 100000m, Percentage = 66.7m },
                new() { Method = "PayPal", Count = 300, Revenue = 30000m, Percentage = 20m },
                new() { Method = "Stripe", Count = 200, Revenue = 20000m, Percentage = 13.3m }
            ],
            PaymentsByStatus =
            [
                new() { Status = "Completed", Count = 1450, Amount = 145000m, Percentage = 96.7m },
                new() { Status = "Failed", Count = 50, Amount = 5000m, Percentage = 3.3m }
            ],
            DailyPayments =
            [
                new() { Date = DateTime.UtcNow.AddDays(-1), Revenue = 5000m, Refunds = 200m, Chargebacks = 50m, NetRevenue = 4750m },
                new() { Date = DateTime.UtcNow, Revenue = 6000m, Refunds = 300m, Chargebacks = 100m, NetRevenue = 5600m }
            ],
            FailedPaymentReasons =
            [
                new() { Reason = "Insufficient Funds", Count = 20, Percentage = 40m },
                new() { Reason = "Card Declined", Count = 15, Percentage = 30m },
                new() { Reason = "Expired Card", Count = 10, Percentage = 20m },
                new() { Reason = "Other", Count = 5, Percentage = 10m }
            ]
        };

        dto.TotalPayments.Should().Be(1500);
        dto.TotalRevenue.Should().Be(150000m);
        dto.PaymentsByMethod.Should().HaveCount(3);
        dto.PaymentsByStatus.Should().HaveCount(2);
        dto.DailyPayments.Should().HaveCount(2);
        dto.FailedPaymentReasons.Should().HaveCount(4);
    }
}

public class MarketingReportDtoComprehensiveTests
{
    [Fact]
    public void MarketingReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new MarketingReportDto
        {
            TotalCouponsUsed = 500,
            TotalDiscountGiven = 10000m,
            AverageCouponValue = 20m,
            CouponUsageRate = 25m,
            ActivePromotions = 3,
            PromotionRevenue = 50000m,
            TotalBanners = 4,
            TotalBannerImpressions = 50000,
            TotalBannerClicks = 1500,
            BannerClickThroughRate = 3.0m,
            TotalNewsletterSubscribers = 1500,
            NewSubscribersThisMonth = 200,
            UnsubscribesThisMonth = 50,
            Period = "month",
            TopCoupons =
            [
                new() { Code = "SAVE20", UsageCount = 200, TotalDiscount = 4000m, Revenue = 20000m },
                new() { Code = "WELCOME10", UsageCount = 150, TotalDiscount = 1500m, Revenue = 15000m }
            ],
            PromotionPerformance =
            [
                new() { Name = "Summer Sale", UsageCount = 300, Revenue = 30000m, ROI = 3.0m },
                new() { Name = "Flash Sale", UsageCount = 200, Revenue = 20000m, ROI = 2.5m }
            ],
            BannerPerformance =
            [
                new() { Title = "Summer Sale", Impressions = 20000, Clicks = 600, ClickThroughRate = 3.0m, Conversions = 50, Revenue = 5000m },
                new() { Title = "New Arrivals", Impressions = 15000, Clicks = 450, ClickThroughRate = 3.0m, Conversions = 30, Revenue = 3000m }
            ]
        };

        dto.TotalCouponsUsed.Should().Be(500);
        dto.TotalDiscountGiven.Should().Be(10000m);
        dto.TopCoupons.Should().HaveCount(2);
        dto.PromotionPerformance.Should().HaveCount(2);
        dto.BannerPerformance.Should().HaveCount(2);
    }
}

public class ReportSummaryDtoComprehensiveTests
{
    [Fact]
    public void ReportSummaryDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ReportSummaryDto
        {
            Revenue = new RevenueReportDto
            {
                TotalRevenue = 150000m,
                GrossProfit = 60000m,
                GrossMargin = 40m
            },
            Sales = new SalesReportDto
            {
                TotalSales = 1500,
                TotalRevenue = 150000m
            },
            Customers = new CustomerReportDto
            {
                TotalCustomers = 3000,
                CustomerRetentionRate = 83.3m
            },
            Inventory = new InventoryReportDto
            {
                TotalProducts = 250,
                TotalInventoryValue = 500000m
            },
            Payments = new PaymentReportDto
            {
                TotalPayments = 1500,
                TotalRevenue = 150000m
            },
            Marketing = new MarketingReportDto
            {
                TotalCouponsUsed = 500,
                TotalDiscountGiven = 10000m
            }
        };

        dto.Revenue.Should().NotBeNull();
        dto.Sales.Should().NotBeNull();
        dto.Customers.Should().NotBeNull();
        dto.Inventory.Should().NotBeNull();
        dto.Payments.Should().NotBeNull();
        dto.Marketing.Should().NotBeNull();
    }
}
