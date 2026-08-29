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

public class ApiResponseTests
{
    [Fact]
    public void ApiResponse_Success_ShouldReturnTrueWhenSuccess()
    {
        var response = new ApiResponse("Success", true);

        response.Success.Should().BeTrue();
        response.Message.Should().Be("Success");
    }

    [Fact]
    public void ApiResponse_Success_ShouldReturnFalseWhenNotSuccess()
    {
        var response = new ApiResponse("Error", false);

        response.Success.Should().BeFalse();
        response.Message.Should().Be("Error");
    }

    [Fact]
    public void ApiResponse_Errors_ShouldBeEmptyByDefault()
    {
        var response = new ApiResponse("Success", true);

        response.Errors.Should().NotBeNull();
        response.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ApiResponse_Errors_ShouldBeSettable()
    {
        var response = new ApiResponse("Error", false, ["Error 1", "Error 2"]);

        response.Errors.Should().HaveCount(2);
        response.Errors.Should().Contain("Error 1");
        response.Errors.Should().Contain("Error 2");
    }

    [Fact]
    public void ApiResponse_Timestamp_ShouldBeSetToUtcNow()
    {
        var before = DateTime.UtcNow;
        var response = new ApiResponse("Success", true);
        var after = DateTime.UtcNow;

        response.Timestamp.Should().BeOnOrAfter(before);
        response.Timestamp.Should().BeOnOrBefore(after);
    }
}

public class ApiResponseGenericTests
{
    [Fact]
    public void ApiResponse_T_Success_ShouldReturnData()
    {
        var response = new ApiResponse<string>("Success", true, "test data");

        response.Success.Should().BeTrue();
        response.Data.Should().Be("test data");
    }

    [Fact]
    public void ApiResponse_T_Success_ShouldReturnNullData()
    {
        var response = new ApiResponse<string>("Success", true);

        response.Data.Should().BeNull();
    }

    [Fact]
    public void ApiResponse_T_Success_ShouldReturnListData()
    {
        var data = new List<string> { "item1", "item2", "item3" };
        var response = new ApiResponse<List<string>>("Success", true, data);

        response.Data.Should().HaveCount(3);
        response.Data.Should().Contain("item1");
    }
}

public class PagedResponseTests
{
    [Fact]
    public void PagedResponse_ShouldReturnPagedData()
    {
        var data = new List<string> { "item1", "item2", "item3" };
        var response = new PagedResponse<List<string>>(data, 1, 10, 100);

        response.Data.Should().HaveCount(3);
        response.Page.Should().Be(1);
        response.PageSize.Should().Be(10);
        response.TotalCount.Should().Be(100);
    }

    [Fact]
    public void PagedResponse_TotalPages_ShouldCalculateCorrectly()
    {
        var response = new PagedResponse<List<string>>([], 1, 10, 100);

        response.TotalPages.Should().Be(10);
    }

    [Fact]
    public void PagedResponse_HasPreviousPage_ShouldReturnFalseWhenFirstPage()
    {
        var response = new PagedResponse<List<string>>([], 1, 10, 100);

        response.HasPreviousPage.Should().BeFalse();
    }

    [Fact]
    public void PagedResponse_HasPreviousPage_ShouldReturnTrueWhenNotFirstPage()
    {
        var response = new PagedResponse<List<string>>([], 2, 10, 100);

        response.HasPreviousPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_HasNextPage_ShouldReturnTrueWhenHasMorePages()
    {
        var response = new PagedResponse<List<string>>([], 1, 10, 100);

        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_HasNextPage_ShouldReturnFalseWhenLastPage()
    {
        var response = new PagedResponse<List<string>>([], 10, 10, 100);

        response.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public void PagedResponse_IsEmpty_ShouldReturnTrueWhenNoData()
    {
        var response = new PagedResponse<List<string>>([], 1, 10, 0);

        response.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_IsEmpty_ShouldReturnFalseWhenHasData()
    {
        var response = new PagedResponse<List<string>>(["item1"], 1, 10, 1);

        response.IsEmpty.Should().BeFalse();
    }
}

public class WrapperTests
{
    [Fact]
    public void Wrapper_Success_ShouldReturnSuccessResponse()
    {
        var response = Wrapper.Success("Operation completed");

        response.Success.Should().BeTrue();
        response.Message.Should().Be("Operation completed");
    }

    [Fact]
    public void Wrapper_SuccessWithData_ShouldReturnData()
    {
        var response = Wrapper.Success("Operation completed", "test data");

        response.Success.Should().BeTrue();
        response.Message.Should().Be("Operation completed");
    }

    [Fact]
    public void Wrapper_Error_ShouldReturnErrorResponse()
    {
        var response = Wrapper.Error("Operation failed");

        response.Success.Should().BeFalse();
        response.Message.Should().Be("Operation failed");
    }

    [Fact]
    public void Wrapper_ErrorWithErrors_ShouldReturnErrors()
    {
        var response = Wrapper.Error("Operation failed", ["Error 1", "Error 2"]);

        response.Success.Should().BeFalse();
        response.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void Wrapper_NotFound_ShouldReturnNotFoundResponse()
    {
        var response = Wrapper.NotFound("Resource not found");

        response.Success.Should().BeFalse();
        response.Message.Should().Be("Resource not found");
    }

    [Fact]
    public void Wrapper_Unauthorized_ShouldReturnUnauthorizedResponse()
    {
        var response = Wrapper.Unauthorized("Unauthorized access");

        response.Success.Should().BeFalse();
        response.Message.Should().Be("Unauthorized access");
    }

    [Fact]
    public void Wrapper_Forbidden_ShouldReturnForbiddenResponse()
    {
        var response = Wrapper.Forbidden("Forbidden access");

        response.Success.Should().BeFalse();
        response.Message.Should().Be("Forbidden access");
    }

    [Fact]
    public void Wrapper_ValidationError_ShouldReturnValidationErrors()
    {
        var response = Wrapper.ValidationError(["Error 1", "Error 2"]);

        response.Success.Should().BeFalse();
        response.Errors.Should().HaveCount(2);
    }
}

public class ProductDtoExtendedTests
{
    [Fact]
    public void ProductCollectionDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductCollectionDto
        {
            Id = Guid.NewGuid(),
            Name = "Summer Collection",
            Description = "Summer products",
            ImageUrl = "https://example.com/collection.jpg",
            DisplayOrder = 1,
            IsActive = true,
            ProductCount = 25,
            Products =
            [
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 49.99m },
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 29.99m }
            ]
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Summer Collection");
        dto.Products.Should().HaveCount(2);
    }
}

public class ProductTagDtoTests
{
    [Fact]
    public void ProductTagDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductTagDto
        {
            Id = Guid.NewGuid(),
            Name = "wireless",
            Slug = "wireless",
            ProductCount = 50
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("wireless");
        dto.Slug.Should().Be("wireless");
        dto.ProductCount.Should().Be(50);
    }
}

public class CreateProductCollectionRequestTests
{
    [Fact]
    public void CreateProductCollectionRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateProductCollectionRequest
        {
            Name = "Summer Collection",
            Description = "Summer products",
            ImageUrl = "https://example.com/collection.jpg",
            DisplayOrder = 1,
            IsActive = true,
            ProductIds = [Guid.NewGuid(), Guid.NewGuid()]
        };

        request.Name.Should().Be("Summer Collection");
        request.ProductIds.Should().HaveCount(2);
    }
}

public class ProductComparisonRequestTests
{
    [Fact]
    public void ProductComparisonRequest_ProductIds_ShouldBeSettable()
    {
        var request = new ProductComparisonRequest
        {
            ProductIds = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()]
        };

        request.ProductIds.Should().HaveCount(3);
    }
}

public class ProductComparisonResultTests
{
    [Fact]
    public void ProductComparisonResult_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductComparisonResult
        {
            Products =
            [
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 99.99m },
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 149.99m }
            ],
            Attributes = ["Price", "Rating", "Stock", "Brand"]
        };

        dto.Products.Should().HaveCount(2);
        dto.Attributes.Should().HaveCount(4);
    }
}

public class ProductRecommendationDtoTests
{
    [Fact]
    public void ProductRecommendationDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductRecommendationDto
        {
            Products =
            [
                new() { Id = Guid.NewGuid(), Name = "Recommended 1", Price = 49.99m, Reason = "Based on your browsing history" },
                new() { Id = Guid.NewGuid(), Name = "Recommended 2", Price = 29.99m, Reason = "Customers also bought" }
            ],
            Type = "Personalized",
            TotalCount = 2
        };

        dto.Products.Should().HaveCount(2);
        dto.Type.Should().Be("Personalized");
        dto.TotalCount.Should().Be(2);
    }
}

public class ProductBulkOperationRequestTests
{
    [Fact]
    public void ProductBulkOperationRequest_AllProperties_ShouldBeSettable()
    {
        var request = new ProductBulkOperationRequest
        {
            ProductIds = [Guid.NewGuid(), Guid.NewGuid()],
            Operation = "UpdatePrice",
            Parameters = new Dictionary<string, string> { ["Price"] = "49.99" }
        };

        request.ProductIds.Should().HaveCount(2);
        request.Operation.Should().Be("UpdatePrice");
        request.Parameters.Should().ContainKey("Price");
    }
}

public class StockMovementDtoExtendedTests
{
    [Fact]
    public void StockMovementReportDto_AllProperties_ShouldBeSettable()
    {
        var dto = new StockMovementReportDto
        {
            TotalInbound = 500,
            TotalOutbound = 300,
            TotalAdjustments = 10,
            NetMovement = 190,
            Movements =
            [
                new() { Date = DateTime.UtcNow.AddDays(-1), Inbound = 100, Outbound = 50, Adjustments = 5 },
                new() { Date = DateTime.UtcNow, Inbound = 120, Outbound = 60, Adjustments = 3 }
            ]
        };

        dto.TotalInbound.Should().Be(500);
        dto.TotalOutbound.Should().Be(300);
        dto.NetMovement.Should().Be(190);
        dto.Movements.Should().HaveCount(2);
    }
}
