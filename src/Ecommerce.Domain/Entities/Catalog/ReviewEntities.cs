using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Catalog;

public class ProductReview : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Pros { get; set; }
    public string? Cons { get; set; }
    public bool IsApproved { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public bool IsDeleted { get; set; }
    public int HelpfulVotes { get; set; }
    public int NotHelpfulVotes { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual User.ApplicationUser User { get; set; } = null!;
    public virtual ICollection<ReviewImage> Images { get; set; } = new List<ReviewImage>();
    public virtual ICollection<ReviewHelpfulnessVote> HelpfulnessVotes { get; set; } = new List<ReviewHelpfulnessVote>();
    public virtual ICollection<ReviewReport> Reports { get; set; } = new List<ReviewReport>();
}

public class ReviewImage : BaseEntity
{
    public Guid ReviewId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ProductReview Review { get; set; } = null!;
}

public class ReviewHelpfulnessVote : BaseEntity
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }
    public bool IsHelpful { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ProductReview Review { get; set; } = null!;
    public virtual User.ApplicationUser User { get; set; } = null!;
}

public class ReviewReport : BaseEntity
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? AdditionalDetails { get; set; }
    public bool IsResolved { get; set; }
    public string? ResolvedBy { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ProductReview Review { get; set; } = null!;
    public virtual User.ApplicationUser User { get; set; } = null!;
}
