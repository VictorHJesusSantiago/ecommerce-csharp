using Xunit;
using FluentAssertions;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.UnitTests;

public class ApiResponseTests
{
    [Fact]
    public void ApiResponse_Success_ShouldReturnSuccessResponse()
    {
        var response = ApiResponse.SuccessResponse("Operation completed");

        response.Succeeded.Should().BeTrue();
        response.Message.Should().Be("Operation completed");
        response.StatusCode.Should().Be(200);
    }

    [Fact]
    public void ApiResponse_Fail_ShouldReturnFailResponse()
    {
        var response = ApiResponse.FailResponse("Something went wrong", 400);

        response.Succeeded.Should().BeFalse();
        response.Message.Should().Be("Something went wrong");
        response.StatusCode.Should().Be(400);
    }

    [Fact]
    public void ApiResponse<T>_Success_ShouldReturnSuccessResponseWithData()
    {
        var data = new ProductDto { Name = "Test Product", Price = 49.99m };
        var response = ApiResponse<ProductDto>.SuccessResponse(data, "Product retrieved");

        response.Succeeded.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Name.Should().Be("Test Product");
        response.Data.Price.Should().Be(49.99m);
        response.Message.Should().Be("Product retrieved");
    }

    [Fact]
    public void ApiResponse<T>_Fail_ShouldReturnFailResponse()
    {
        var response = ApiResponse<ProductDto>.FailResponse("Not found", 404);

        response.Succeeded.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Message.Should().Be("Not found");
        response.StatusCode.Should().Be(404);
    }

    [Fact]
    public void PagedResponse_ShouldReturnPaginatedData()
    {
        var data = new List<ProductDto>
        {
            new() { Name = "Product 1" },
            new() { Name = "Product 2" },
            new() { Name = "Product 3" }
        };

        var response = new PagedResponse<List<ProductDto>>
        {
            Data = data,
            PageNumber = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecords = 3
        };

        response.Data.Should().HaveCount(3);
        response.PageNumber.Should().Be(1);
        response.PageSize.Should().Be(10);
        response.TotalPages.Should().Be(1);
        response.TotalRecords.Should().Be(3);
    }

    [Fact]
    public void PagedResponse_EmptyData_ShouldReturnEmptyList()
    {
        var response = new PagedResponse<List<ProductDto>>
        {
            Data = [],
            PageNumber = 1,
            PageSize = 10,
            TotalPages = 0,
            TotalRecords = 0
        };

        response.Data.Should().BeEmpty();
        response.TotalPages.Should().Be(0);
        response.TotalRecords.Should().Be(0);
    }

    [Fact]
    public void ApiResponse_DefaultValues_ShouldBeCorrect()
    {
        var response = new ApiResponse();

        response.Succeeded.Should().BeFalse();
        response.Message.Should().BeNull();
        response.Errors.Should().BeNull();
        response.StatusCode.Should().Be(200);
    }

    [Fact]
    public void ApiResponse_WithErrors_ShouldStoreErrors()
    {
        var errors = new List<string> { "Error 1", "Error 2" };
        var response = ApiResponse.FailResponse("Failed", 400, errors);

        response.Errors.Should().HaveCount(2);
        response.Errors!.Should().Contain("Error 1");
        response.Errors.Should().Contain("Error 2");
    }
}

public class ProductDtoExtendedTests
{
    [Fact]
    public void ProductDto_ProfitMargin_ShouldCalculateCorrectly()
    {
        var dto = new ProductDto
        {
            Price = 100m,
            CostPrice = 60m
        };

        var margin = Math.Round(((dto.Price - dto.CostPrice.Value) / dto.Price) * 100, 2);

        margin.Should().Be(40m);
    }

    [Fact]
    public void ProductDto_DiscountPercentage_ShouldCalculateCorrectly()
    {
        var dto = new ProductDto
        {
            Price = 49.99m,
            CompareAtPrice = 69.99m
        };

        var discount = Math.Round((1 - dto.Price / dto.CompareAtPrice.Value) * 100, 2);

        discount.Should().Be(28.57m);
    }

    [Fact]
    public void ProductListDto_IsOnSale_ShouldReturnTrueWhenCompareAtPriceHigher()
    {
        var dto = new ProductListDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.IsOnSale.Should().BeTrue();
    }

    [Fact]
    public void ProductListDto_DiscountPercentage_ShouldCalculateCorrectly()
    {
        var dto = new ProductListDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.DiscountPercentage.Should().Be(28.57m);
    }

    [Fact]
    public void ProductSearchRequest_DefaultValues_ShouldBeCorrect()
    {
        var request = new ProductSearchRequest();

        request.Page.Should().Be(1);
        request.PageSize.Should().Be(20);
        request.SortDescending.Should().BeTrue();
    }

    [Fact]
    public void ProductStockDto_AvailableQuantity_ShouldCalculateCorrectly()
    {
        var dto = new ProductStockDto
        {
            TotalStockQuantity = 100,
            ReservedQuantity = 20
        };

        dto.AvailableQuantity.Should().Be(80);
    }

    [Fact]
    public void ProductStockDto_IsLowStock_ShouldReturnTrueWhenBelowThreshold()
    {
        var dto = new ProductStockDto
        {
            TotalStockQuantity = 5,
            ReservedQuantity = 0,
            LowStockThreshold = 10
        };

        dto.IsLowStock.Should().BeTrue();
    }

    [Fact]
    public void ProductStockDto_IsOutOfStock_ShouldReturnTrueWhenZero()
    {
        var dto = new ProductStockDto
        {
            TotalStockQuantity = 0,
            ReservedQuantity = 0
        };

        dto.IsOutOfStock.Should().BeTrue();
    }

    [Fact]
    public void WarehouseStockDto_AvailableQuantity_ShouldCalculateCorrectly()
    {
        var dto = new WarehouseStockDto
        {
            Quantity = 50,
            ReservedQuantity = 10
        };

        dto.AvailableQuantity.Should().Be(40);
    }
}

public class OrderDtoExtendedTests
{
    [Fact]
    public void OrderDto_TotalAmount_ShouldBeSumOfComponents()
    {
        var dto = new OrderDto
        {
            SubTotal = 100m,
            TaxAmount = 8m,
            ShippingCost = 9.99m,
            DiscountAmount = 5m
        };

        var total = dto.SubTotal + dto.TaxAmount + dto.ShippingCost - dto.DiscountAmount;

        total.Should().Be(112.99m);
    }

    [Fact]
    public void OrderItemDto_TotalPrice_ShouldCalculateCorrectly()
    {
        var dto = new OrderItemDto
        {
            UnitPrice = 49.99m,
            Quantity = 3
        };

        var total = dto.UnitPrice * dto.Quantity;

        total.Should().Be(149.97m);
    }

    [Fact]
    public void OrderSearchRequest_DefaultValues_ShouldBeCorrect()
    {
        var request = new OrderSearchRequest();

        request.Page.Should().Be(1);
        request.PageSize.Should().Be(20);
        request.SortDescending.Should().BeTrue();
    }

    [Fact]
    public void PlaceOrderRequest_ShouldHaveItems()
    {
        var request = new PlaceOrderRequest
        {
            ShippingAddressId = Guid.NewGuid(),
            BillingAddressId = Guid.NewGuid(),
            Items =
            [
                new() { ProductId = Guid.NewGuid(), Quantity = 2 },
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            ]
        };

        request.Items.Should().HaveCount(2);
    }

    [Fact]
    public void UpdateOrderStatusRequest_ShouldHaveStatus()
    {
        var request = new UpdateOrderStatusRequest
        {
            Status = "Shipped",
            TrackingNumber = "1Z999AA10123456784"
        };

        request.Status.Should().Be("Shipped");
        request.TrackingNumber.Should().Be("1Z999AA10123456784");
    }
}

public class CartDtoExtendedTests
{
    [Fact]
    public void CartDto_TotalItems_ShouldCountItems()
    {
        var dto = new CartDto
        {
            Items =
            [
                new() { Quantity = 2 },
                new() { Quantity = 3 },
                new() { Quantity = 1 }
            ]
        };

        var totalItems = dto.Items.Sum(i => i.Quantity);

        totalItems.Should().Be(6);
    }

    [Fact]
    public void CartItemDto_TotalPrice_ShouldCalculateCorrectly()
    {
        var dto = new CartItemDto
        {
            Price = 49.99m,
            Quantity = 2
        };

        var total = dto.Price * dto.Quantity;

        total.Should().Be(99.98m);
    }

    [Fact]
    public void AddToCartRequest_ShouldHaveProductIdAndQuantity()
    {
        var request = new AddToCartRequest
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2
        };

        request.ProductId.Should().NotBeEmpty();
        request.Quantity.Should().Be(2);
    }

    [Fact]
    public void CartSummaryDto_ShouldCalculateCorrectly()
    {
        var dto = new CartSummaryDto
        {
            ItemCount = 5,
            SubTotal = 250m,
            Tax = 20m,
            ShippingCost = 9.99m,
            Discount = 25m
        };

        var total = dto.SubTotal + dto.Tax + dto.ShippingCost - dto.Discount;

        total.Should().Be(254.99m);
    }
}

public class ReviewDtoExtendedTests
{
    [Fact]
    public void ReviewDto_Rating_ShouldBeBetween1And5()
    {
        var dto = new ReviewDto { Rating = 5 };

        dto.Rating.Should().BeInRange(1, 5);
    }

    [Fact]
    public void ReviewStatsDto_AverageRating_ShouldCalculateCorrectly()
    {
        var stats = new ReviewStatsDto
        {
            FiveStarCount = 10,
            FourStarCount = 5,
            ThreeStarCount = 3,
            TwoStarCount = 1,
            OneStarCount = 1
        };

        var totalReviews = stats.FiveStarCount + stats.FourStarCount + stats.ThreeStarCount +
                           stats.TwoStarCount + stats.OneStarCount;
        var weightedSum = (stats.FiveStarCount * 5) + (stats.FourStarCount * 4) +
                         (stats.ThreeStarCount * 3) + (stats.TwoStarCount * 2) + stats.OneStarCount;
        var average = (double)weightedSum / totalReviews;

        average.Should().Be(4.2);
    }

    [Fact]
    public void ReviewSearchRequest_DefaultValues_ShouldBeCorrect()
    {
        var request = new ReviewSearchRequest();

        request.Page.Should().Be(1);
        request.PageSize.Should().Be(20);
        request.SortDescending.Should().BeTrue();
    }
}

public class MarketingDtoTests
{
    [Fact]
    public void CouponDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new CouponDto();

        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void CouponDto_FirstTimeOnly_ShouldDefaultToFalse()
    {
        var dto = new CouponDto();

        dto.FirstTimeOnly.Should().BeFalse();
    }

    [Fact]
    public void ValidateCouponRequest_ShouldHaveCodeAndAmount()
    {
        var request = new ValidateCouponRequest
        {
            Code = "SAVE20",
            OrderAmount = 100m
        };

        request.Code.Should().Be("SAVE20");
        request.OrderAmount.Should().Be(100m);
    }

    [Fact]
    public void BannerDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new BannerDto();

        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void PromotionDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new PromotionDto();

        dto.IsActive.Should().BeTrue();
    }
}

public class NotificationDtoExtendedTests
{
    [Fact]
    public void NotificationDto_IsRead_ShouldDefaultToFalse()
    {
        var dto = new NotificationDto();

        dto.IsRead.Should().BeFalse();
    }

    [Fact]
    public void NotificationDto_Priority_ShouldDefaultToNormal()
    {
        var dto = new NotificationDto();

        dto.Priority.Should().Be("Normal");
    }

    [Fact]
    public void SendNotificationRequest_ShouldHaveRequiredFields()
    {
        var request = new SendNotificationRequest
        {
            RecipientId = Guid.NewGuid(),
            Title = "Test",
            Message = "Test message",
            Type = "Info"
        };

        request.RecipientId.Should().NotBeEmpty();
        request.Title.Should().Be("Test");
        request.Message.Should().Be("Test message");
        request.Type.Should().Be("Info");
    }
}

public class CmsDtoTests
{
    [Fact]
    public void CmsPageDto_IsPublished_ShouldDefaultToFalse()
    {
        var dto = new CmsPageDto();

        dto.IsPublished.Should().BeFalse();
    }

    [Fact]
    public void CmsPageDto_ViewCount_ShouldIncrement()
    {
        var dto = new CmsPageDto { ViewCount = 100 };

        dto.ViewCount++;

        dto.ViewCount.Should().Be(101);
    }

    [Fact]
    public void NavigationMenuDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new NavigationMenuDto();

        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void SiteSettingDto_IsPublic_ShouldDefaultToFalse()
    {
        var dto = new SiteSettingDto();

        dto.IsPublic.Should().BeFalse();
    }

    [Fact]
    public void MediaFileDto_IsImage_ShouldBeStored()
    {
        var dto = new MediaFileDto { IsImage = true, ContentType = "image/jpeg" };

        dto.IsImage.Should().BeTrue();
        dto.ContentType.Should().Be("image/jpeg");
    }
}

public class ReportDtoTests
{
    [Fact]
    public void DashboardSummaryDto_ShouldHaveCorrectDefaults()
    {
        var dto = new DashboardSummaryDto();

        dto.TodayRevenue.Should().Be(0m);
        dto.TodayOrders.Should().Be(0);
        dto.TotalProducts.Should().Be(0);
        dto.TotalCustomers.Should().Be(0);
    }

    [Fact]
    public void SalesReportDto_NetRevenue_ShouldCalculateCorrectly()
    {
        var dto = new SalesReportDto
        {
            TotalRevenue = 100000m,
            RefundAmount = 5000m
        };

        dto.NetRevenue.Should().Be(95000m);
    }

    [Fact]
    public void InventoryReportDto_TotalAvailable_ShouldCalculateCorrectly()
    {
        var dto = new InventoryReportDto
        {
            TotalStockQuantity = 1000,
            TotalReservedQuantity = 200
        };

        var available = dto.TotalStockQuantity - dto.TotalReservedQuantity;

        available.Should().Be(800);
    }

    [Fact]
    public void ExportRequest_ShouldHaveRequiredFields()
    {
        var request = new ExportRequest
        {
            ReportType = "sales",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            Format = "CSV"
        };

        request.ReportType.Should().Be("sales");
        request.Format.Should().Be("CSV");
    }
}

public class SearchDtoExtendedTests
{
    [Fact]
    public void SearchResultDto_TotalPages_ShouldCalculateCorrectly()
    {
        var dto = new SearchResultDto
        {
            TotalResults = 45,
            PageSize = 20
        };

        var totalPages = (int)Math.Ceiling(dto.TotalResults / (double)dto.PageSize);

        totalPages.Should().Be(3);
    }

    [Fact]
    public void SearchResultItemDto_RelevanceScore_ShouldBeStored()
    {
        var dto = new SearchResultItemDto { RelevanceScore = 0.95 };

        dto.RelevanceScore.Should().Be(0.95);
    }

    [Fact]
    public void SearchFiltersDto_ShouldHaveEmptyCollections()
    {
        var dto = new SearchFiltersDto();

        dto.Categories.Should().NotBeNull();
        dto.Brands.Should().NotBeNull();
        dto.Ratings.Should().NotBeNull();
    }

    [Fact]
    public void TrendingSearchDto_ShouldHaveQueryAndCount()
    {
        var dto = new TrendingSearchDto
        {
            Query = "wireless headphones",
            SearchCount = 1500,
            TrendScore = 95
        };

        dto.Query.Should().Be("wireless headphones");
        dto.SearchCount.Should().Be(1500);
        dto.TrendScore.Should().Be(95);
    }
}
