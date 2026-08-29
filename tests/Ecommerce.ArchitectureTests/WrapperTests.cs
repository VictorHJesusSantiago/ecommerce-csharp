using FluentAssertions;
using Xunit;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.DTOs.Common;

namespace Ecommerce.ArchitectureTests;

public class WrapperTests
{
    [Fact]
    public void ApiResponse_ShouldCreateSuccessResponse()
    {
        var response = ApiResponse.Success("Done");
        response.Success.Should().BeTrue();
        response.Message.Should().Be("Done");
        response.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ApiResponse_ShouldCreateErrorResponse()
    {
        var response = ApiResponse.Error("Something failed");
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Something failed");
    }

    [Fact]
    public void ApiResponse_ShouldCreateErrorResponseWithErrors()
    {
        var errors = new List<string> { "Error 1", "Error 2" };
        var response = ApiResponse.Error("Validation failed", errors);
        response.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void ApiResponse_T_ShouldWrapData()
    {
        var data = new { Id = 1, Name = "Test" };
        var response = ApiResponse<object>.Success(data, "Loaded");
        response.Data.Should().NotBeNull();
        response.Success.Should().BeTrue();
    }

    [Fact]
    public void ApiResponse_T_ShouldWrapList()
    {
        var items = new List<string> { "a", "b", "c" };
        var response = ApiResponse<List<string>>.Success(items, "Found 3 items");
        response.Data.Should().HaveCount(3);
    }

    [Fact]
    public void PagedResponse_ShouldHavePaginationMetadata()
    {
        var items = new List<int> { 1, 2, 3 };
        var response = new PagedResponse<int>
        {
            Success = true,
            Data = items,
            TotalCount = 30,
            CurrentPage = 1,
            PageSize = 10,
            TotalPages = 3
        };

        response.HasPreviousPage.Should().BeFalse();
        response.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void PagedResponse_ShouldCalculateTotalPages()
    {
        var response = new PagedResponse<int>
        {
            TotalCount = 95,
            PageSize = 10
        };

        response.TotalPages.Should().Be(10);
    }

    [Fact]
    public void ApiResponse_ShouldCreateFromException()
    {
        var ex = new Exception("Something went wrong");
        var response = ApiResponse.Error(ex.Message);
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Something went wrong");
    }

    [Fact]
    public void ApiResponse_WithMetadata_ShouldStoreMetadata()
    {
        var response = new ApiResponse
        {
            Success = true,
            Message = "OK",
            Metadata = new Dictionary<string, object>
            {
                { "requestId", Guid.NewGuid() },
                { "processingTime", 45 }
            }
        };

        response.Metadata.Should().ContainKey("requestId");
    }
}
