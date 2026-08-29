using Xunit;
using FluentAssertions;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;

namespace Ecommerce.UnitTests;

public class ReviewDtoComprehensiveTests
{
    [Fact]
    public void ReviewDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ReviewDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UserFullName = "John Doe",
            UserAvatarUrl = "https://example.com/avatar.jpg",
            Rating = 4,
            Title = "Great product",
            Comment = "Really enjoyed this product",
            Pros = ["Quality", "Value"],
            Cons = ["Packaging"],
            IsVerifiedPurchase = true,
            HelpfulCount = 10,
            ReportCount = 0,
            Images =
            [
                new() { Url = "https://example.com/review1.jpg", AltText = "Review image 1" },
                new() { Url = "https://example.com/review2.jpg", AltText = "Review image 2" }
            ],
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Rating.Should().Be(4);
        dto.Title.Should().Be("Great product");
        dto.Comment.Should().Be("Really enjoyed this product");
        dto.Pros.Should().HaveCount(2);
        dto.Cons.Should().HaveCount(1);
        dto.IsVerifiedPurchase.Should().BeTrue();
        dto.Images.Should().HaveCount(2);
    }

    [Fact]
    public void ReviewDto_IsPositive_ShouldReturnTrueWhenRatingAbove3()
    {
        var dto = new ReviewDto { Rating = 4 };

        dto.IsPositive.Should().BeTrue();
    }

    [Fact]
    public void ReviewDto_IsPositive_ShouldReturnFalseWhenRatingBelow3()
    {
        var dto = new ReviewDto { Rating = 2 };

        dto.IsPositive.Should().BeFalse();
    }

    [Fact]
    public void ReviewDto_HasImages_ShouldReturnTrueWhenHasImages()
    {
        var dto = new ReviewDto
        {
            Images = [new() { Url = "https://example.com/image.jpg" }]
        };

        dto.HasImages.Should().BeTrue();
    }

    [Fact]
    public void ReviewDto_HasImages_ShouldReturnFalseWhenNoImages()
    {
        var dto = new ReviewDto
        {
            Images = []
        };

        dto.HasImages.Should().BeFalse();
    }
}

public class ReviewImageDtoComprehensiveTests
{
    [Fact]
    public void ReviewImageDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ReviewImageDto
        {
            Id = Guid.NewGuid(),
            Url = "https://example.com/review-image.jpg",
            AltText = "Review image"
        };

        dto.Id.Should().NotBeEmpty();
        dto.Url.Should().Be("https://example.com/review-image.jpg");
        dto.AltText.Should().Be("Review image");
    }
}

public class ReviewHelpfulnessDtoComprehensiveTests
{
    [Fact]
    public void ReviewHelpfulnessDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ReviewHelpfulnessDto
        {
            ReviewId = Guid.NewGuid(),
            HelpfulCount = 10,
            NotHelpfulCount = 2,
            IsHelpful = true
        };

        dto.ReviewId.Should().NotBeEmpty();
        dto.HelpfulCount.Should().Be(10);
        dto.NotHelpfulCount.Should().Be(2);
        dto.IsHelpful.Should().BeTrue();
    }
}

public class CreateReviewRequestComprehensiveTests
{
    [Fact]
    public void CreateReviewRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateReviewRequest
        {
            ProductId = Guid.NewGuid(),
            Rating = 5,
            Title = "Excellent!",
            Comment = "Best product I've ever bought",
            Pros = ["Quality", "Value", "Design"],
            Cons = ["Price"],
            Images =
            [
                new() { Url = "https://example.com/image1.jpg", AltText = "Image 1" }
            ]
        };

        request.ProductId.Should().NotBeEmpty();
        request.Rating.Should().Be(5);
        request.Title.Should().Be("Excellent!");
        request.Comment.Should().Be("Best product I've ever bought");
        request.Pros.Should().HaveCount(3);
        request.Cons.Should().HaveCount(1);
        request.Images.Should().HaveCount(1);
    }
}

public class UpdateReviewRequestComprehensiveTests
{
    [Fact]
    public void UpdateReviewRequest_AllProperties_ShouldBeOptional()
    {
        var request = new UpdateReviewRequest();

        request.Rating.Should().BeNull();
        request.Title.Should().BeNull();
        request.Comment.Should().BeNull();
        request.Pros.Should().BeNull();
        request.Cons.Should().BeNull();
    }

    [Fact]
    public void UpdateReviewRequest_WithValues_ShouldSetValues()
    {
        var request = new UpdateReviewRequest
        {
            Rating = 4,
            Title = "Updated title",
            Comment = "Updated comment"
        };

        request.Rating.Should().Be(4);
        request.Title.Should().Be("Updated title");
        request.Comment.Should().Be("Updated comment");
    }
}

public class ReviewSearchRequestComprehensiveTests
{
    [Fact]
    public void ReviewSearchRequest_DefaultValues_ShouldBeCorrect()
    {
        var request = new ReviewSearchRequest();

        request.Page.Should().Be(1);
        request.PageSize.Should().Be(20);
        request.ProductId.Should().BeNull();
        request.UserId.Should().BeNull();
        request.Rating.Should().BeNull();
        request.IsVerifiedPurchase.Should().BeNull();
        request.SortBy.Should().BeNull();
        request.SortDescending.Should().BeTrue();
    }

    [Fact]
    public void ReviewSearchRequest_WithFilters_ShouldSetFilters()
    {
        var request = new ReviewSearchRequest
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Rating = 5,
            IsVerifiedPurchase = true,
            SortBy = "date",
            SortDescending = false,
            Page = 2,
            PageSize = 10
        };

        request.ProductId.Should().NotBeNull();
        request.UserId.Should().NotBeNull();
        request.Rating.Should().Be(5);
        request.IsVerifiedPurchase.Should().BeTrue();
        request.SortBy.Should().Be("date");
        request.SortDescending.Should().BeFalse();
        request.Page.Should().Be(2);
        request.PageSize.Should().Be(10);
    }
}

public class ReviewStatsDtoComprehensiveTests
{
    [Fact]
    public void ReviewStatsDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ReviewStatsDto
        {
            ProductId = Guid.NewGuid(),
            TotalReviews = 128,
            AverageRating = 4.5,
            RatingDistribution = new Dictionary<int, int>
            {
                [1] = 5,
                [2] = 8,
                [3] = 15,
                [4] = 40,
                [5] = 60
            },
            VerifiedPurchasePercentage = 85.5m
        };

        dto.ProductId.Should().NotBeEmpty();
        dto.TotalReviews.Should().Be(128);
        dto.AverageRating.Should().Be(4.5);
        dto.RatingDistribution.Should().HaveCount(5);
        dto.VerifiedPurchasePercentage.Should().Be(85.5m);
    }

    [Fact]
    public void ReviewStatsDto_FiveStarPercentage_ShouldCalculateCorrectly()
    {
        var dto = new ReviewStatsDto
        {
            TotalReviews = 100,
            RatingDistribution = new Dictionary<int, int>
            {
                [1] = 5,
                [2] = 10,
                [3] = 15,
                [4] = 30,
                [5] = 40
            }
        };

        dto.FiveStarPercentage.Should().Be(40m);
    }
}

public class ReportReviewRequestComprehensiveTests
{
    [Fact]
    public void ReportReviewRequest_AllProperties_ShouldBeSettable()
    {
        var request = new ReportReviewRequest
        {
            ReviewId = Guid.NewGuid(),
            Reason = "Spam",
            Description = "This review is spam"
        };

        request.ReviewId.Should().NotBeEmpty();
        request.Reason.Should().Be("Spam");
        request.Description.Should().Be("This review is spam");
    }
}
