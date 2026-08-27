namespace Ecommerce.Web.Models.Account;

public class PaymentMethodViewModel
{
    public Guid Id { get; set; }
    public string CardType { get; set; } = string.Empty;
    public string LastFourDigits { get; set; } = string.Empty;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CardholderName { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsExpired => ExpiryYear < DateTime.UtcNow.Year || (ExpiryYear == DateTime.UtcNow.Year && ExpiryMonth < DateTime.UtcNow.Month);
    public string DisplayName => $"{CardType} ending in {LastFourDigits}";
}

public class AddPaymentMethodViewModel
{
    public string CardNumber { get; set; } = string.Empty;
    public string CardholderName { get; set; } = string.Empty;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CVV { get; set; } = string.Empty;
    public bool SaveAsDefault { get; set; }
    public bool SaveForFuture { get; set; } = true;
}
