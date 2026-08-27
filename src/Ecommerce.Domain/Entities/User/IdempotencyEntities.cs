using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.User;

public class OrderIdempotencyRecord : BaseEntity
{
    public string IdempotencyKey { get; set; } = string.Empty;
    public string RequestBody { get; set; } = string.Empty;
    public string? ResponseBody { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
}
