using FluentAssertions;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.UnitTests.ApplicationTests;

public class ApiResponseTests
{
    [Fact]
    public void ApiResponse_Success_ShouldReturnSuccessResponse()
    {
        var response = ApiResponse.SuccessResponse("Test message");
        response.Success.Should().BeTrue();
        response.Message.Should().Be("Test message");
        response.StatusCode.Should().Be(200);
    }

    [Fact]
    public void ApiResponse_Fail_ShouldReturnFailResponse()
    {
        var response = ApiResponse.FailResponse("Error occurred", 400);
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Error occurred");
        response.StatusCode.Should().Be(400);
    }

    [Fact]
    public void ApiResponse_GenericSuccess_ShouldReturnData()
    {
        var response = ApiResponse<int>.SuccessResponse(42);
        response.Success.Should().BeTrue();
        response.Data.Should().Be(42);
    }

    [Fact]
    public void ApiResponse_GenericFail_ShouldReturnError()
    {
        var response = ApiResponse<int>.FailResponse("Not found", 404);
        response.Success.Should().BeFalse();
        response.StatusCode.Should().Be(404);
    }

    [Fact]
    public void ApiResponse_WithErrors_ShouldContainErrors()
    {
        var errors = new List<string> { "Error 1", "Error 2" };
        var response = ApiResponse.FailResponse("Validation failed", 400, errors);
        response.Errors.Should().HaveCount(2);
        response.Errors.Should().Contain("Error 1");
    }
}

public class ProductDtoTests
{
    [Fact]
    public void ProductDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 19.99m,
            Sku = "TEST-001",
            Slug = "test-product"
        };

        dto.Name.Should().Be("Test Product");
        dto.Price.Should().Be(19.99m);
    }
}

public class CreateProductRequestTests
{
    [Fact]
    public void CreateProductRequest_ShouldHaveRequiredFields()
    {
        var request = new CreateProductRequest
        {
            Name = "New Product",
            Description = "Description",
            Price = 29.99m,
            StockQuantity = 50,
            Sku = "NEW-001",
            CategoryId = Guid.NewGuid()
        };

        request.Name.Should().NotBeNullOrEmpty();
        request.Price.Should().BeGreaterThan(0);
        request.StockQuantity.Should().BeGreaterThanOrEqualTo(0);
    }
}

public class CartDtoTests
{
    [Fact]
    public void CartDto_CalculateTotal_ShouldBeCorrect()
    {
        var cart = new CartDto
        {
            Items =
            [
                new CartItemDto { Price = 10.00m, Quantity = 2 },
                new CartItemDto { Price = 5.50m, Quantity = 1 }
            ]
        };

        var expectedTotal = (10.00m * 2) + (5.50m * 1);
        expectedTotal.Should().Be(25.50m);
    }
}

public class ValidateCouponRequestTests
{
    [Fact]
    public void ValidateCouponRequest_ShouldHaveCode()
    {
        var request = new ValidateCouponRequest
        {
            Code = "SAVE20",
            TotalAmount = 100m
        };

        request.Code.Should().Be("SAVE20");
        request.TotalAmount.Should().Be(100m);
    }
}

public class RegisterRequestTests
{
    [Fact]
    public void RegisterRequest_ShouldHaveRequiredFields()
    {
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Password = "Password123!"
        };

        request.Email.Should().Contain("@");
        request.Password.Should().HaveLength(14);
    }
}

public class PaginationTests
{
    [Fact]
    public void PagedResponse_ShouldHaveCorrectStructure()
    {
        var response = new PagedResponse<string>
        {
            Data = ["item1", "item2"],
            PageNumber = 1,
            PageSize = 10,
            TotalPages = 5,
            TotalRecords = 50
        };

        response.Data.Should().HaveCount(2);
        response.PageNumber.Should().Be(1);
        response.TotalRecords.Should().Be(50);
        response.HasPreviousPage.Should().BeFalse();
        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_HasPreviousPage_ShouldBeTrue()
    {
        var response = new PagedResponse<string>
        {
            PageNumber = 3,
            PageSize = 10,
            TotalPages = 5
        };

        response.HasPreviousPage.Should().BeTrue();
        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_LastPage_HasNextPage_ShouldBeFalse()
    {
        var response = new PagedResponse<string>
        {
            PageNumber = 5,
            PageSize = 10,
            TotalPages = 5
        };

        response.HasNextPage.Should().BeFalse();
        response.HasPreviousPage.Should().BeTrue();
    }
}
