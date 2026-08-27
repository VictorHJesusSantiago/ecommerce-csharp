using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Review;

public class ProductReview : AggregateRoot
{
    public Guid ProductId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public int Rating { get; private set; }
    public ReviewStatus Status { get; private set; }
    public string? Pros { get; private set; }
    public string? Cons { get; private set; }
    public bool IsVerifiedPurchase { get; private set; }
    public Guid? OrderId { get; private set; }
    public int HelpfulCount { get; private set; }
    public int NotHelpfulCount { get; private set; }
    public int ReportCount { get; private set; }
    public string? AdminResponse { get; private set; }
    public DateTime? AdminResponseAt { get; private set; }
    public string? ResponseAuthor { get; private set; }

    private readonly List<ReviewImage> _images = [];
    public IReadOnlyCollection<ReviewImage> Images => _images.AsReadOnly();

    private readonly List<ReviewHelpfulnessVote> _votes = [];
    public IReadOnlyCollection<ReviewHelpfulnessVote> Votes => _votes.AsReadOnly();

    private ProductReview() { }

    public static ProductReview Create(
        Guid productId, Guid userId, string title, string body, int rating,
        string? pros = null, string? cons = null,
        bool isVerifiedPurchase = false, Guid? orderId = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Review title is required.", nameof(title));
        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException("Review body is required.", nameof(body));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));

        return new ProductReview
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            UserId = userId,
            Title = title.Trim(),
            Body = body.Trim(),
            Rating = rating,
            Status = ReviewStatus.Pending,
            Pros = pros?.Trim(),
            Cons = cons?.Trim(),
            IsVerifiedPurchase = isVerifiedPurchase,
            OrderId = orderId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Approve(string? approvedBy = null)
    {
        Status = ReviewStatus.Approved;
        UpdateTimestamp();
    }

    public void Reject(string? reason = null)
    {
        Status = ReviewStatus.Rejected;
        UpdateTimestamp();
    }

    public void Flag()
    {
        Status = ReviewStatus.Flagged;
        ReportCount++;
        UpdateTimestamp();
    }

    public void Hide()
    {
        Status = ReviewStatus.Hidden;
        UpdateTimestamp();
    }

    public void Respond(string response, string? author = null)
    {
        if (string.IsNullOrWhiteSpace(response))
            throw new ArgumentException("Response is required.", nameof(response));
        AdminResponse = response.Trim();
        AdminResponseAt = DateTime.UtcNow;
        ResponseAuthor = author?.Trim();
        UpdateTimestamp();
    }

    public void AddImage(ReviewImage image)
    {
        if (image is null) throw new ArgumentNullException(nameof(image));
        _images.Add(image);
        UpdateTimestamp();
    }

    public void RemoveImage(Guid imageId)
    {
        var image = _images.FirstOrDefault(i => i.Id == imageId);
        if (image is not null)
        {
            _images.Remove(image);
            UpdateTimestamp();
        }
    }

    public bool Vote(Guid userId, bool isHelpful)
    {
        var existingVote = _votes.FirstOrDefault(v => v.UserId == userId);
        if (existingVote is not null)
            return false;

        var vote = ReviewHelpfulnessVote.Create(Id, userId, isHelpful);
        _votes.Add(vote);

        if (isHelpful) HelpfulCount++;
        else NotHelpfulCount++;

        UpdateTimestamp();
        return true;
    }

    public double GetHelpfulnessScore()
    {
        var total = HelpfulCount + NotHelpfulCount;
        if (total == 0) return 0;
        return Math.Round((double)HelpfulCount / total * 100, 2);
    }

    public void UpdateReview(string title, string body, int rating,
        string? pros = null, string? cons = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Review title is required.", nameof(title));
        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException("Review body is required.", nameof(body));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));

        Title = title.Trim();
        Body = body.Trim();
        Rating = rating;
        Pros = pros?.Trim();
        Cons = cons?.Trim();
        UpdateTimestamp();
    }
}

public class ReviewImage : BaseEntity
{
    public Guid ReviewId { get; private set; }
    public string Url { get; private set; } = string.Empty;
    public string? AltText { get; private set; }
    public int DisplayOrder { get; private set; }
    public ProductReview Review { get; private set; } = null!;

    private ReviewImage() { }

    public static ReviewImage Create(Guid reviewId, string url, string? altText = null, int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Image URL is required.", nameof(url));
        return new ReviewImage
        {
            Id = Guid.NewGuid(),
            ReviewId = reviewId,
            Url = url,
            AltText = altText?.Trim(),
            DisplayOrder = displayOrder,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class ReviewHelpfulnessVote : BaseEntity
{
    public Guid ReviewId { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsHelpful { get; private set; }
    public ProductReview Review { get; private set; } = null!;

    private ReviewHelpfulnessVote() { }

    public static ReviewHelpfulnessVote Create(Guid reviewId, Guid userId, bool isHelpful)
    {
        return new ReviewHelpfulnessVote
        {
            Id = Guid.NewGuid(),
            ReviewId = reviewId,
            UserId = userId,
            IsHelpful = isHelpful,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class ReviewReport : BaseEntity
{
    public Guid ReviewId { get; private set; }
    public Guid ReportedBy { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public string? Details { get; private set; }
    public bool IsResolved { get; private set; }
    public string? ResolvedBy { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public string? ResolutionNotes { get; private set; }
    public ProductReview Review { get; private set; } = null!;

    private ReviewReport() { }

    public static ReviewReport Create(Guid reviewId, Guid reportedBy, string reason, string? details = null)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason is required.", nameof(reason));
        return new ReviewReport
        {
            Id = Guid.NewGuid(),
            ReviewId = reviewId,
            ReportedBy = reportedBy,
            Reason = reason.Trim(),
            Details = details?.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Resolve(string resolvedBy, string? notes = null)
    {
        IsResolved = true;
        ResolvedBy = resolvedBy;
        ResolvedAt = DateTime.UtcNow;
        ResolutionNotes = notes;
        UpdateTimestamp();
    }
}
