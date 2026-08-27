using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.Application.Validators.Ordering;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order number is required")
            .MaximumLength(50).WithMessage("Order number cannot exceed 50 characters");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than 0");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must have at least one item");
    }
}

public class PlaceOrderRequestValidator : AbstractValidator<PlaceOrderRequest>
{
    public PlaceOrderRequestValidator()
    {
        RuleFor(x => x.ShippingAddressId)
            .NotEmpty().WithMessage("Shipping address is required");

        RuleFor(x => x.BillingAddressId)
            .NotEmpty().WithMessage("Billing address is required");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must have at least one item");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("Product ID is required");
            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        });
    }
}

public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
{
    public UpdateOrderStatusRequestValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(50).WithMessage("Status cannot exceed 50 characters");
    }
}

public class OrderSearchRequestValidator : AbstractValidator<OrderSearchRequest>
{
    public OrderSearchRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100");

        RuleFor(x => x.Status)
            .MaximumLength(50).WithMessage("Status cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Status));
    }
}

public class OrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string? UserEmail { get; set; }
    public string? UserFullName { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? PaymentStatus { get; set; }
    public string? PaymentMethod { get; set; }
    public AddressDto? ShippingAddress { get; set; }
    public AddressDto? BillingAddress { get; set; }
    public string? Notes { get; set; }
    public string? CouponCode { get; set; }
    public List<OrderHistoryDto> History { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductImage { get; set; }
    public string? Sku { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? DiscountAmount { get; set; }
}

public class AddressDto
{
    public Guid Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string? Street2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Phone { get; set; }
}

public class OrderHistoryDto
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PlaceOrderRequest
{
    public Guid ShippingAddressId { get; set; }
    public Guid BillingAddressId { get; set; }
    public string? Notes { get; set; }
    public string? CouponCode { get; set; }
    public string? PaymentMethod { get; set; }
    public List<PlaceOrderItemRequest> Items { get; set; } = [];
}

public class PlaceOrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid? ProductVariantId { get; set; }
}

public class UpdateOrderStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
}

public class OrderSearchRequest
{
    public string? SearchTerm { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? UserId { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public enum OrderStatusEnum
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Returned,
    Refunded
}
