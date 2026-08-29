using Xunit;
using FluentAssertions;

namespace Ecommerce.UnitTests;

public class BehaviorValidationTests
{
    [Fact]
    public void ApiResponseWrapper_Success_ShouldCreateSuccessResponse()
    {
        var response = Ecommerce.Application.Wrappers.Wrapper.Success("Done");
        response.Success.Should().BeTrue();
        response.Message.Should().Be("Done");
    }

    [Fact]
    public void ApiResponseWrapper_Error_ShouldCreateErrorResponse()
    {
        var response = Ecommerce.Application.Wrappers.Wrapper.Error("Failed");
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Failed");
    }

    [Fact]
    public void ApiResponseWrapper_NotFound_ShouldCreateNotFoundResponse()
    {
        var response = Ecommerce.Application.Wrappers.Wrapper.NotFound("Not found");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public void ApiResponseWrapper_Unauthorized_ShouldCreateUnauthorizedResponse()
    {
        var response = Ecommerce.Application.Wrappers.Wrapper.Unauthorized("Unauthorized");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public void ApiResponseWrapper_Forbidden_ShouldCreateForbiddenResponse()
    {
        var response = Ecommerce.Application.Wrappers.Wrapper.Forbidden("Forbidden");
        response.Success.Should().BeFalse();
    }

    [Fact]
    public void ApiResponseWrapper_ValidationError_ShouldCreateValidationResponse()
    {
        var errors = new[] { "Error 1", "Error 2" };
        var response = Ecommerce.Application.Wrappers.Wrapper.ValidationError(errors);
        response.Success.Should().BeFalse();
        response.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void ApiResponseWrapper_SuccessWithData_ShouldContainData()
    {
        var data = new List<string> { "item1", "item2" };
        var response = Ecommerce.Application.Wrappers.Wrapper.Success("Done", data);
        response.Success.Should().BeTrue();
    }
}

public class ProductFactoryTests
{
    [Fact]
    public void Create_ShouldReturnProductWithCorrectValues()
    {
        var product = Ecommerce.Domain.Factories.ProductFactory.Create(
            "Test Product", "Description", 49.99m, "SKU-001", Guid.NewGuid(), 100);

        product.Name.Should().Be("Test Product");
        product.Price.Amount.Should().Be(49.99m);
        product.Sku.Value.Should().Be("SKU-001");
        product.StockQuantity.Should().Be(100);
        product.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Create_ShouldGenerateSlug()
    {
        var product = Ecommerce.Domain.Factories.ProductFactory.Create(
            "Test Product", "Description", 49.99m, "SKU-001", Guid.NewGuid(), 100);

        product.Slug.Value.Should().Be("test-product");
    }

    [Fact]
    public void Create_WithNullName_ShouldThrow()
    {
        Action act = () => Ecommerce.Domain.Factories.ProductFactory.Create(
            null!, "Description", 49.99m, "SKU-001", Guid.NewGuid(), 100);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Create_WithNegativePrice_ShouldThrow()
    {
        Action act = () => Ecommerce.Domain.Factories.ProductFactory.Create(
            "Test", "Desc", -10m, "SKU-001", Guid.NewGuid(), 100);

        act.Should().Throw<Exception>();
    }
}

public class OrderFactoryTests
{
    [Fact]
    public void Create_ShouldReturnOrderWithCorrectValues()
    {
        var order = Ecommerce.Domain.Factories.OrderFactory.Create(
            Guid.NewGuid(), Guid.NewGuid(), "ORD-001", 100m, "USD");

        order.OrderNumber.Should().Be("ORD-001");
        order.Status.Should().Be(Ecommerce.Domain.Enums.OrderStatus.Pending);
    }

    [Fact]
    public void Create_ShouldSetCreatedAt()
    {
        var before = DateTime.UtcNow;
        var order = Ecommerce.Domain.Factories.OrderFactory.Create(
            Guid.NewGuid(), Guid.NewGuid(), "ORD-001", 100m, "USD");
        var after = DateTime.UtcNow;

        order.CreatedAt.Should().BeOnOrAfter(before);
        order.CreatedAt.Should().BeOnOrBefore(after);
    }
}

public class CartFactoryTests
{
    [Fact]
    public void Create_ShouldReturnCartWithCorrectValues()
    {
        var cart = Ecommerce.Domain.Factories.CartFactory.Create(Guid.NewGuid());
        cart.UserId.Should().NotBeEmpty();
        cart.Items.Should().BeEmpty();
    }
}

public class CategoryFactoryTests
{
    [Fact]
    public void Create_ShouldReturnCategoryWithCorrectValues()
    {
        var category = Ecommerce.Domain.Factories.CategoryFactory.Create("Electronics", "Description");
        category.Name.Should().Be("Electronics");
        category.Slug.Value.Should().Be("electronics");
        category.IsActive.Should().BeTrue();
    }
}

public class DomainExceptionTests
{
    [Fact]
    public void NotFoundException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.NotFoundException("Product", Guid.NewGuid());
        ex.Message.Should().Contain("Product");
    }

    [Fact]
    public void BadRequestException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.BadRequestException("Bad request");
        ex.Message.Should().Be("Bad request");
    }

    [Fact]
    public void ConflictException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.ConflictException("Conflict");
        ex.Message.Should().Be("Conflict");
    }

    [Fact]
    public void UnauthorizedDomainException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.UnauthorizedDomainException("Unauthorized");
        ex.Message.Should().Be("Unauthorized");
    }

    [Fact]
    public void ForbiddenDomainException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.ForbiddenDomainException("Forbidden");
        ex.Message.Should().Be("Forbidden");
    }

    [Fact]
    public void ValidationDomainException_ShouldContainErrors()
    {
        var errors = new List<string> { "Error 1", "Error 2" };
        var ex = new Ecommerce.Domain.Exceptions.ValidationDomainException(errors);
        ex.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void ConcurrencyException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.ConcurrencyException();
        ex.Message.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void RateLimitExceededException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.RateLimitExceededException();
        ex.Message.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ExternalServiceException_ShouldHaveCorrectMessage()
    {
        var ex = new Ecommerce.Domain.Exceptions.ExternalServiceException("Stripe", "Service unavailable");
        ex.Service.Should().Be("Stripe");
    }
}
