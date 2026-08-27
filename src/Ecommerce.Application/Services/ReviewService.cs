using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities.Catalog;
using System.Linq.Expressions;

namespace Ecommerce.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepo;
    private readonly IProductRepository _productRepo;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(IReviewRepository reviewRepo, IProductRepository productRepo, ILogger<ReviewService> logger)
    {
        _reviewRepo = reviewRepo;
        _productRepo = productRepo;
        _logger = logger;
    }

    public async Task<ApiResponse<List<ReviewDto>>> GetProductReviewsAsync(Guid productId, CancellationToken ct = default)
    {
        var reviews = await _reviewRepo.FindAsync(r => r.ProductId == productId && r.IsApproved, ct);
        var dtos = reviews.OrderByDescending(r => r.CreatedAt).Select(MapToDto).ToList();
        return ApiResponse<List<ReviewDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<ReviewDto>> GetReviewByIdAsync(Guid reviewId, CancellationToken ct = default)
    {
        var review = await _reviewRepo.GetByIdAsync(reviewId, ct);
        if (review is null)
            return ApiResponse<ReviewDto>.FailResponse("Review not found.", 404);
        return ApiResponse<ReviewDto>.SuccessResponse(MapToDto(review));
    }

    public async Task<ApiResponse<ReviewDto>> CreateReviewAsync(Guid userId, CreateReviewRequest request, CancellationToken ct = default)
    {
        var existingReviews = await _reviewRepo.FindAsync(r => r.ProductId == request.ProductId && r.UserId == userId, ct);
        if (existingReviews.Any())
            return ApiResponse<ReviewDto>.FailResponse("You have already reviewed this product.", 400);

        var review = new ProductReview
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            UserId = userId,
            Title = request.Title,
            Comment = request.Comment,
            Rating = request.Rating,
            Pros = request.Pros,
            Cons = request.Cons,
            IsApproved = false,
            HelpfulVotes = 0,
            CreatedAt = DateTime.UtcNow
        };

        await _reviewRepo.AddAsync(review, ct);
        _logger.LogInformation("Review created: {ReviewId} for product {ProductId}", review.Id, request.ProductId);

        return ApiResponse<ReviewDto>.SuccessResponse(MapToDto(review));
    }

    public async Task<ApiResponse<ReviewDto>> UpdateReviewAsync(Guid reviewId, Guid userId, UpdateReviewRequest request, CancellationToken ct = default)
    {
        var review = await _reviewRepo.GetByIdAsync(reviewId, ct);
        if (review is null)
            return ApiResponse<ReviewDto>.FailResponse("Review not found.", 404);

        if (review.UserId != userId)
            return ApiResponse<ReviewDto>.FailResponse("You can only edit your own reviews.", 403);

        review.Title = request.Title ?? review.Title;
        review.Comment = request.Comment ?? review.Comment;
        review.Rating = request.Rating ?? review.Rating;

        await _reviewRepo.UpdateAsync(review, ct);
        _logger.LogInformation("Review updated: {ReviewId}", reviewId);

        return ApiResponse<ReviewDto>.SuccessResponse(MapToDto(review));
    }

    public async Task<ApiResponse> DeleteReviewAsync(Guid reviewId, Guid userId, CancellationToken ct = default)
    {
        var review = await _reviewRepo.GetByIdAsync(reviewId, ct);
        if (review is null)
            return ApiResponse.FailResponse("Review not found.", 404);

        if (review.UserId != userId)
            return ApiResponse.FailResponse("You can only delete your own reviews.", 403);

        review.IsDeleted = true;
        await _reviewRepo.UpdateAsync(review, ct);
        _logger.LogInformation("Review deleted: {ReviewId}", reviewId);

        return ApiResponse.SuccessResponse("Review deleted successfully.");
    }

    public async Task<ApiResponse> ApproveReviewAsync(Guid reviewId, CancellationToken ct = default)
    {
        var review = await _reviewRepo.GetByIdAsync(reviewId, ct);
        if (review is null)
            return ApiResponse.FailResponse("Review not found.", 404);

        review.IsApproved = true;
        review.ApprovedAt = DateTime.UtcNow;
        await _reviewRepo.UpdateAsync(review, ct);

        return ApiResponse.SuccessResponse("Review approved successfully.");
    }

    public async Task<ApiResponse> VoteReviewAsync(Guid reviewId, Guid userId, bool isHelpful, CancellationToken ct = default)
    {
        var review = await _reviewRepo.GetByIdAsync(reviewId, ct);
        if (review is null)
            return ApiResponse.FailResponse("Review not found.", 404);

        if (isHelpful)
            review.HelpfulVotes++;
        else
            review.NotHelpfulVotes++;

        await _reviewRepo.UpdateAsync(review, ct);
        return ApiResponse.SuccessResponse("Vote recorded.");
    }

    public async Task<ApiResponse<ReviewStatsDto>> GetReviewStatsAsync(Guid productId, CancellationToken ct = default)
    {
        var reviews = await _reviewRepo.FindAsync(r => r.ProductId == productId && r.IsApproved, ct);
        var reviewList = reviews.ToList();
        var stats = new ReviewStatsDto
        {
            TotalReviews = reviewList.Count,
            AverageRating = reviewList.Any() ? Math.Round(reviewList.Average(r => r.Rating), 1) : 0,
            RatingDistribution = Enumerable.Range(1, 5).ToDictionary(
                r => r,
                r => reviewList.Count(rev => rev.Rating == r)
            )
        };
        return ApiResponse<ReviewStatsDto>.SuccessResponse(stats);
    }

    private static ReviewDto MapToDto(ProductReview r) => new()
    {
        Id = r.Id,
        ProductId = r.ProductId,
        UserId = r.UserId,
        Title = r.Title,
        Comment = r.Comment,
        Rating = r.Rating,
        Pros = r.Pros,
        Cons = r.Cons,
        HelpfulVotes = r.HelpfulVotes,
        IsApproved = r.IsApproved,
        CreatedAt = r.CreatedAt
    };
}
