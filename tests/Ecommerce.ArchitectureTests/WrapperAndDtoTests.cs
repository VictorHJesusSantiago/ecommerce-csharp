using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Common;

namespace Ecommerce.ArchitectureTests;

public class WrapperAndDtoTests
{
    [Fact]
    public void ApiResponse_ShouldHaveRequiredProperties()
    {
        var response = new ApiResponse
        {
            Success = true,
            Message = "Operation successful",
            Errors = new List<string>(),
            Timestamp = DateTime.UtcNow
        };

        response.Success.Should().BeTrue();
        response.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ApiResponse_WithError_ShouldHaveErrors()
    {
        var response = new ApiResponse
        {
            Success = false,
            Message = "Validation failed",
            Errors = new List<string> { "Name is required", "Price must be positive" }
        };

        response.Success.Should().BeFalse();
        response.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void ApiResponse_T_ShouldContainData()
    {
        var response = new ApiResponse<string>
        {
            Success = true,
            Message = "OK",
            Data = "hello world"
        };

        response.Data.Should().Be("hello world");
    }

    [Fact]
    public void PagedResponse_ShouldHavePaginationInfo()
    {
        var response = new PagedResponse<int>
        {
            Success = true,
            Data = new List<int> { 1, 2, 3 },
            TotalCount = 30,
            CurrentPage = 2,
            PageSize = 10,
            TotalPages = 3,
            HasPreviousPage = true,
            HasNextPage = true
        };

        response.TotalPages.Should().Be(3);
        response.HasPreviousPage.Should().BeTrue();
        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_FirstPage_ShouldNotHavePrevious()
    {
        var response = new PagedResponse<int>
        {
            CurrentPage = 1,
            TotalPages = 5,
            PageSize = 10
        };

        response.HasPreviousPage.Should().BeFalse();
        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_LastPage_ShouldNotHaveNext()
    {
        var response = new PagedResponse<int>
        {
            CurrentPage = 5,
            TotalPages = 5,
            PageSize = 10
        };

        response.HasPreviousPage.Should().BeTrue();
        response.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public void AddressDto_ShouldHaveRequiredProperties()
    {
        var dto = new AddressDto
        {
            Street = "123 Main St",
            Street2 = "Apt 4",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "US",
            Phone = "+1234567890"
        };

        dto.FullAddress.Should().Contain("Main St");
        dto.FullAddress.Should().Contain("New York");
    }

    [Fact]
    public void MoneyDto_ShouldHaveRequiredProperties()
    {
        var dto = new MoneyDto
        {
            Amount = 99.99m,
            Currency = "USD",
            DisplayAmount = "$99.99"
        };

        dto.Amount.Should().Be(99.99m);
        dto.DisplayAmount.Should().Be("$99.99");
    }

    [Fact]
    public void DateRangeDto_ShouldHaveRequiredProperties()
    {
        var dto = new DateRangeDto
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31),
            Label = "Year 2024"
        };

        dto.StartDate.Should().Be(new DateTime(2024, 1, 1));
        dto.Label.Should().Be("Year 2024");
    }

    [Fact]
    public void ValidationError_ShouldHaveRequiredProperties()
    {
        var error = new ValidationError
        {
            Field = "Email",
            Message = "Email is required",
            AttemptedValue = "",
            Severity = "Error"
        };

        error.Field.Should().Be("Email");
        error.Severity.Should().Be("Error");
    }

    [Fact]
    public void PaginationParams_ShouldHaveDefaults()
    {
        var pagination = new PaginationParams();
        pagination.Page.Should().Be(1);
        pagination.PageSize.Should().Be(20);
    }

    [Fact]
    public void SortParams_ShouldHaveRequiredProperties()
    {
        var sort = new SortParams
        {
            SortBy = "Name",
            SortOrder = "asc"
        };

        sort.SortBy.Should().Be("Name");
        sort.SortOrder.Should().Be("asc");
    }
}
