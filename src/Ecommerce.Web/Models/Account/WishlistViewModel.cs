namespace Ecommerce.Web.Models.Account;

public class WishlistViewModel
{
    public List<WishlistItemViewModel> Items { get; set; } = new();
    public int TotalItems => Items.Count;
    public decimal TotalValue => Items.Sum(i => i.Price);
}

public class WishlistItemViewModel
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductImage { get; set; }
    public string? Brand { get; set; }
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public bool IsOnSale => OriginalPrice.HasValue && OriginalPrice > Price;
    public decimal DiscountPercent => IsOnSale ? Math.Round((1 - Price / OriginalPrice!.Value) * 100) : 0;
    public bool IsInStock { get; set; }
    public bool IsAvailable => IsInStock;
    public DateTime AddedAt { get; set; }
    public string? Notes { get; set; }
}
