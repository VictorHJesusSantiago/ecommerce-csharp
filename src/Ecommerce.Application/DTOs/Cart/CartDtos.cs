namespace Ecommerce.Application.DTOs.Cart;

public class CartDto
{
    public Guid Id { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? CouponCode { get; set; }
    public int TotalItems => Items.Sum(i => i.Quantity);
}

public class CartItemDto
{
    public Guid ProductId { get; set; }
    public string? VariantId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public string? ImageUrl { get; set; }
    public bool InStock { get; set; }
    public int MaxQuantity { get; set; }
}

public class AddToCartRequest
{
    public Guid ProductId { get; set; }
    public string? VariantId { get; set; }
    public int Quantity { get; set; } = 1;
}

public class UpdateCartItemRequest
{
    public int Quantity { get; set; }
}

public class ApplyCouponRequest
{
    public string CouponCode { get; set; } = string.Empty;
}
