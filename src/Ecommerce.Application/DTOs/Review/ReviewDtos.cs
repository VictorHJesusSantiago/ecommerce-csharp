namespace Ecommerce.Application.DTOs.Review;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Pros { get; set; }
    public string? Cons { get; set; }
    public bool IsApproved { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public int HelpfulVotes { get; set; }
    public int NotHelpfulVotes { get; set; }
    public List<ReviewImageDto> Images { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class ReviewImageDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
}

public class CreateReviewRequest
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Pros { get; set; }
    public string? Cons { get; set; }
    public List<string> ImageUrls { get; set; } = [];
}

public class UpdateReviewRequest
{
    public string? Title { get; set; }
    public string? Comment { get; set; }
    public int? Rating { get; set; }
    public string? Pros { get; set; }
    public string? Cons { get; set; }
}

public class VoteReviewRequest
{
    public bool IsHelpful { get; set; }
}

public class ReviewStatsDto
{
    public int TotalReviews { get; set; }
    public double AverageRating { get; set; }
    public Dictionary<int, int> RatingDistribution { get; set; } = [];
    public int FiveStarCount => RatingDistribution.TryGetValue(5, out var count) ? count : 0;
    public int FourStarCount => RatingDistribution.TryGetValue(4, out var count) ? count : 0;
    public int ThreeStarCount => RatingDistribution.TryGetValue(3, out var count) ? count : 0;
    public int TwoStarCount => RatingDistribution.TryGetValue(2, out var count) ? count : 0;
    public int OneStarCount => RatingDistribution.TryGetValue(1, out var count) ? count : 0;
}

public class ReviewSearchRequest
{
    public Guid? ProductId { get; set; }
    public Guid? UserId { get; set; }
    public int? MinRating { get; set; }
    public int? MaxRating { get; set; }
    public bool? IsApproved { get; set; }
    public string? SearchQuery { get; set; }
    public string? SortBy { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
