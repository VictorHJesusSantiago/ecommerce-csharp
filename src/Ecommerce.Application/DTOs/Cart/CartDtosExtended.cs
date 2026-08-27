namespace Ecommerce.Application.DTOs.Cart;

public class CartCheckoutDto
{
    public Guid CartId { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public List<AddressDto> ShippingAddresses { get; set; } = [];
    public List<AddressDto> BillingAddresses { get; set; } = [];
    public List<PaymentMethodDto> PaymentMethods { get; set; } = [];
    public List<CouponDto> AvailableCoupons { get; set; } = [];
    public string? AppliedCouponCode { get; set; }
    public decimal? CouponDiscount { get; set; }
    public string? SelectedShippingMethod { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public decimal? Savings { get; set; }
    public bool FreeShipping { get; set; }
    public decimal AmountToFreeShipping { get; set; }
    public string? GiftMessage { get; set; }
    public bool IsGift { get; set; }
}

public class ShippingMethodDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Carrier { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public decimal? FreeShippingThreshold { get; set; }
    public TimeSpan EstimatedDelivery { get; set; }
    public string? EstimatedDeliveryText { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsDefault { get; set; }
    public List<string> SupportedCountries { get; set; } = [];
    public string? TrackingUrl { get; set; }
    public int DisplayOrder { get; set; }
}

public class CalculateShippingRequest
{
    public Guid AddressId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public decimal OrderTotal { get; set; }
}

public class ShippingCalculationResult
{
    public List<ShippingMethodDto> Methods { get; set; } = [];
    public decimal CheapestCost { get; set; }
    public decimal FastestDeliveryHours { get; set; }
    public bool FreeShippingAvailable { get; set; }
    public decimal AmountForFreeShipping { get; set; }
}

public class CartHistoryDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public string? CouponCode { get; set; }
    public decimal? CouponDiscount { get; set; }
    public string Action { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? AbandonedAt { get; set; }
    public bool IsRecovered { get; set; }
    public DateTime? RecoveredAt { get; set; }
}

public class CartAbandonmentDto
{
    public int TotalAbandoned { get; set; }
    public int Recovered { get; set; }
    public decimal RecoveryRate { get; set; }
    public decimal AbandonedValue { get; set; }
    public decimal RecoveredValue { get; set; }
    public List<AbandonedCartDto> RecentAbandoned { get; set; } = [];
}

public class AbandonedCartDto
{
    public Guid CartId { get; set; }
    public string? UserEmail { get; set; }
    public int ItemCount { get; set; }
    public decimal CartValue { get; set; }
    public DateTime AbandonedAt { get; set; }
    public int HoursSinceAbandoned { get; set; }
    public bool ReminderSent { get; set; }
    public DateTime? ReminderSentAt { get; set; }
    public bool IsRecovered { get; set; }
    public DateTime? RecoveredAt { get; set; }
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
    public string? Label { get; set; }
    public bool IsDefault { get; set; }
    public string? FullName { get; set; }
    public string? Company { get; set; }
}

public class PaymentMethodDto
{
    public Guid Id { get; set; }
    public string CardType { get; set; } = string.Empty;
    public string Last4Digits { get; set; } = string.Empty;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string? CardHolderName { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CouponDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public bool IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
