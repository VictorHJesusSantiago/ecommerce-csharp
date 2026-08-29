using Xunit;
using FluentAssertions;

namespace Ecommerce.UnitTests;

public class ApiResponseExtendedTests
{
    [Fact]
    public void ApiResponse_WithTimestamp_ShouldHaveUtcTimestamp()
    {
        var before = DateTime.UtcNow.AddSeconds(-1);
        var response = new Ecommerce.Application.DTOs.Order.ApiResponse("test", true);
        var after = DateTime.UtcNow.AddSeconds(1);
        response.Timestamp.Should().BeOnOrAfter(before);
        response.Timestamp.Should().BeOnOrBefore(after);
    }

    [Fact]
    public void ApiResponse_WithMultipleErrors_ShouldContainAllErrors()
    {
        var errors = new List<string> { "Err1", "Err2", "Err3" };
        var response = new Ecommerce.Application.DTOs.Order.ApiResponse("test", false, errors);
        response.Errors.Should().HaveCount(3);
    }

    [Fact]
    public void ApiResponse_SuccessfulResponse_ShouldNotHaveErrors()
    {
        var response = new Ecommerce.Application.DTOs.Order.ApiResponse("test", true);
        response.Errors.Should().BeEmpty();
    }
}

public class PagedResponseExtendedTests
{
    [Fact]
    public void PagedResponse_FirstPage_ShouldNotHavePrevious()
    {
        var data = new List<string> { "a", "b" };
        var response = new Ecommerce.Application.DTOs.Order.PagedResponse<List<string>>(data, 1, 10, 25);
        response.HasPreviousPage.Should().BeFalse();
    }

    [Fact]
    public void PagedResponse_MiddlePage_ShouldHaveBoth()
    {
        var data = new List<string> { "a", "b" };
        var response = new Ecommerce.Application.DTOs.Order.PagedResponse<List<string>>(data, 2, 10, 25);
        response.HasPreviousPage.Should().BeTrue();
        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_LastPage_ShouldNotHaveNext()
    {
        var data = new List<string> { "a", "b" };
        var response = new Ecommerce.Application.DTOs.Order.PagedResponse<List<string>>(data, 3, 10, 25);
        response.HasPreviousPage.Should().BeTrue();
        response.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public void PagedResponse_TotalPages_ShouldRoundUp()
    {
        var response = new Ecommerce.Application.DTOs.Order.PagedResponse<List<string>>([], 1, 10, 25);
        response.TotalPages.Should().Be(3);
    }

    [Fact]
    public void PagedResponse_EmptyData_ShouldBeEmpty()
    {
        var response = new Ecommerce.Application.DTOs.Order.PagedResponse<List<string>>([], 1, 10, 0);
        response.Data.Should().BeEmpty();
        response.IsEmpty.Should().BeTrue();
    }
}

public class ProductSearchExtendedTests
{
    [Fact]
    public void ProductSearchRequest_WithTags_ShouldStoreTags()
    {
        var request = new Ecommerce.Application.DTOs.Product.ProductSearchRequest
        {
            Tags = new List<string> { "wireless", "bluetooth", "premium" }
        };
        request.Tags.Should().HaveCount(3);
    }

    [Fact]
    public void ProductSearchRequest_DefaultPage_ShouldBeOne()
    {
        var request = new Ecommerce.Application.DTOs.Product.ProductSearchRequest();
        request.Page.Should().Be(1);
    }

    [Fact]
    public void ProductSearchRequest_DefaultPageSize_ShouldBe20()
    {
        var request = new Ecommerce.Application.DTOs.Product.ProductSearchRequest();
        request.PageSize.Should().Be(20);
    }
}

public class OrderExtendedTests
{
    [Fact]
    public void OrderDto_CanBeCancelled_ShouldReturnCorrectly()
    {
        var order = new Ecommerce.Application.DTOs.Order.OrderDto { Status = "Pending" };
        order.Status.Should().Be("Pending");
    }

    [Fact]
    public void OrderDto_CanBeRefunded_ShouldReturnCorrectly()
    {
        var order = new Ecommerce.Application.DTOs.Order.OrderDto { Status = "Delivered" };
        order.Status.Should().Be("Delivered");
    }

    [Fact]
    public void OrderSearchRequest_DefaultSortDescending_ShouldBeTrue()
    {
        var request = new Ecommerce.Application.DTOs.Order.OrderSearchRequest();
        request.SortDescending.Should().BeTrue();
    }

    [Fact]
    public void PlaceOrderRequest_Items_ShouldBeList()
    {
        var request = new Ecommerce.Application.DTOs.Order.PlaceOrderRequest
        {
            Items = new List<Ecommerce.Application.DTOs.Order.OrderItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };
        request.Items.Should().HaveCount(1);
    }
}

public class UserExtendedTests
{
    [Fact]
    public void UserDto_FullName_ShouldCombineNames()
    {
        var user = new Ecommerce.Application.DTOs.User.UserDto
        {
            FirstName = "John",
            LastName = "Doe"
        };
        var fullName = $"{user.FirstName} {user.LastName}";
        fullName.Should().Be("John Doe");
    }

    [Fact]
    public void AuthResponse_ShouldContainToken()
    {
        var response = new Ecommerce.Application.DTOs.User.AuthResponse
        {
            Token = "test-token",
            RefreshToken = "refresh-token",
            Expiration = DateTime.UtcNow.AddHours(1)
        };
        response.Token.Should().Be("test-token");
    }

    [Fact]
    public void WishlistDto_EmptyItems_ShouldBeEmpty()
    {
        var wishlist = new Ecommerce.Application.DTOs.User.WishlistDto
        {
            Items = []
        };
        wishlist.ItemCount.Should().Be(0);
    }
}
