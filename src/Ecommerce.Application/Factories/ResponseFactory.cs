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
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Application.Factories;

public static class ResponseFactory
{
    public static ApiResponse<T> Success<T>(T data, string message = "Success")
    {
        return ApiResponse<T>.SuccessResponse(data, message);
    }

    public static ApiResponse<T> Fail<T>(string message, int statusCode = 400)
    {
        return ApiResponse<T>.FailResponse(message, statusCode);
    }

    public static ApiResponse Success(string message = "Success")
    {
        return ApiResponse.SuccessResponse(message);
    }

    public static ApiResponse Fail(string message, int statusCode = 400)
    {
        return ApiResponse.FailResponse(message, statusCode);
    }

    public static PagedResponse<T> Paginated<T>(List<T> items, int page, int pageSize, int totalCount) where T : class
    {
        return new PagedResponse<T>
        {
            Data = items,
            PageNumber = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            TotalRecords = totalCount
        };
    }
}

public static class ProductDtoFactory
{
    public static ProductDto CreateProductDto(
        string name, decimal price, string sku,
        string? description = null, int stockQuantity = 0,
        Guid? categoryId = null, string? categoryName = null)
    {
        return new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            Sku = sku,
            Slug = name.ToLower().Replace(" ", "-"),
            StockQuantity = stockQuantity,
            CategoryId = categoryId,
            CategoryName = categoryName,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static ProductListDto CreateProductListDto(string name, decimal price, string sku)
    {
        return new ProductListDto
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            Sku = sku,
            Slug = name.ToLower().Replace(" ", "-"),
            InStock = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public static class OrderDtoFactory
{
    public static OrderDto CreateOrderDto(string orderNumber, decimal totalAmount, string status = "Pending")
    {
        return new OrderDto
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            TotalAmount = totalAmount,
            Status = status,
            CreatedAt = DateTime.UtcNow,
            Items = []
        };
    }
}

public static class UserDtoFactory
{
    public static UserDto CreateUserDto(string firstName, string lastName, string email)
    {
        return new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public static class CartDtoFactory
{
    public static CartDto CreateEmptyCart()
    {
        return new CartDto { Items = [], SubTotal = 0, Tax = 0, ShippingCost = 0, Total = 0 };
    }

    public static CartDto CreateCartWithItems(List<CartItemDto> items)
    {
        var subTotal = items.Sum(i => i.Price * i.Quantity);
        var tax = Math.Round(subTotal * 0.08m, 2);
        return new CartDto
        {
            Id = Guid.NewGuid(),
            Items = items,
            SubTotal = subTotal,
            Tax = tax,
            ShippingCost = subTotal >= 50 ? 0 : 9.99m,
            Total = subTotal + tax
        };
    }
}
