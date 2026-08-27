namespace Ecommerce.Web.Models.Account;

public class OrderHistoryViewModel
{
    public List<OrderListItemViewModel> Orders { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalOrders { get; set; }
    public string? StatusFilter { get; set; }
    public string? SortBy { get; set; }
}

public class OrderListItemViewModel
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
    public string? FirstItemImage { get; set; }
    public bool CanCancel { get; set; }
    public bool CanReturn { get; set; }
    public bool CanReview { get; set; }
}

public class OrderDetailViewModel
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string ShippingMethod { get; set; } = string.Empty;
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public AddressViewModel? ShippingAddress { get; set; }
    public AddressViewModel? BillingAddress { get; set; }
    public List<OrderItemViewModel> Items { get; set; } = new();
    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CouponCode { get; set; }
    public List<OrderStatusHistoryViewModel> StatusHistory { get; set; } = new();
    public bool CanCancel { get; set; }
    public bool CanReturn { get; set; }
    public bool CanTrack { get; set; }
}

public class OrderItemViewModel
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductImage { get; set; }
    public string? VariantName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsReviewed { get; set; }
}

public class OrderStatusHistoryViewModel
{
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
}
