namespace Ecommerce.Application.Validators.Marketing;

public class CouponDtoValidator : AbstractValidator<CouponDto>
{
    public CouponDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required")
            .MaximumLength(50).WithMessage("Coupon code cannot exceed 50 characters");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0");

        RuleFor(x => x.UsageLimit)
            .GreaterThan(0).When(x => x.UsageLimit.HasValue)
            .WithMessage("Usage limit must be greater than 0");
    }
}

public class CreateCouponRequestValidator : AbstractValidator<CreateCouponRequest>
{
    public CreateCouponRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required")
            .MaximumLength(50).WithMessage("Coupon code cannot exceed 50 characters")
            .Matches("^[A-Z0-9_-]+$").WithMessage("Coupon code must contain only uppercase letters, numbers, underscores, and hyphens");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.DiscountType)
            .NotEmpty().WithMessage("Discount type is required");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0");

        RuleFor(x => x.MinimumOrderAmount)
            .GreaterThanOrEqualTo(0).When(x => x.MinimumOrderAmount.HasValue)
            .WithMessage("Minimum order amount must be non-negative");

        RuleFor(x => x.MaximumDiscountAmount)
            .GreaterThanOrEqualTo(0).When(x => x.MaximumDiscountAmount.HasValue)
            .WithMessage("Maximum discount amount must be non-negative");

        RuleFor(x => x.UsageLimit)
            .GreaterThan(0).When(x => x.UsageLimit.HasValue)
            .WithMessage("Usage limit must be greater than 0");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).When(x => x.EndDate.HasValue)
            .WithMessage("Start date must be before end date");
    }
}

public class UpdateCouponRequestValidator : AbstractValidator<UpdateCouponRequest>
{
    public UpdateCouponRequestValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0");

        RuleFor(x => x.MinimumOrderAmount)
            .GreaterThanOrEqualTo(0).When(x => x.MinimumOrderAmount.HasValue)
            .WithMessage("Minimum order amount must be non-negative");

        RuleFor(x => x.MaximumDiscountAmount)
            .GreaterThanOrEqualTo(0).When(x => x.MaximumDiscountAmount.HasValue)
            .WithMessage("Maximum discount amount must be non-negative");

        RuleFor(x => x.UsageLimit)
            .GreaterThan(0).When(x => x.UsageLimit.HasValue)
            .WithMessage("Usage limit must be greater than 0");
    }
}

public class ValidateCouponRequestValidator : AbstractValidator<ValidateCouponRequest>
{
    public ValidateCouponRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required");

        RuleFor(x => x.OrderAmount)
            .GreaterThan(0).WithMessage("Order amount must be greater than 0");
    }
}

public class BannerDtoValidator : AbstractValidator<BannerDto>
{
    public BannerDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Banner title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL is required")
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _)).WithMessage("Invalid image URL");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Display order must be non-negative");
    }
}

public class PromotionDtoValidator : AbstractValidator<PromotionDto>
{
    public PromotionDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Promotion name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.DiscountPercentage)
            .InclusiveBetween(0, 100).When(x => x.DiscountPercentage.HasValue)
            .WithMessage("Discount percentage must be between 0 and 100");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("Start date must be before end date");
    }
}

public class CouponDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> ApplicableProductIds { get; set; } = [];
    public List<string> ApplicableCategoryIds { get; set; } = [];
    public bool FirstTimeOnly { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateCouponRequest
{
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "Percentage";
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> ApplicableProductIds { get; set; } = [];
    public List<string> ApplicableCategoryIds { get; set; } = [];
    public bool FirstTimeOnly { get; set; }
}

public class UpdateCouponRequest
{
    public string? Description { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public bool IsActive { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ValidateCouponRequest
{
    public string Code { get; set; } = string.Empty;
    public decimal OrderAmount { get; set; }
    public Guid? UserId { get; set; }
    public List<string> ProductIds { get; set; } = [];
}

public class ValidateCouponResponse
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
    public decimal DiscountAmount { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public string? CouponCode { get; set; }
    public decimal? FinalAmount { get; set; }
}

public class PromotionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string PromotionType { get; set; } = string.Empty;
    public decimal? DiscountPercentage { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> ApplicableProductIds { get; set; } = [];
    public List<string> ApplicableCategoryIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class BannerDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? LinkUrl { get; set; }
    public string Position { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ViewCount { get; set; }
    public int ClickCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class SubscribeNewsletterRequest
{
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class NewsletterSubscriberDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; }
    public DateTime SubscribedAt { get; set; }
    public DateTime? UnsubscribedAt { get; set; }
}

public class DiscountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public bool IsActive { get; set; }
    public List<DiscountTierDto> Tiers { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class DiscountTierDto
{
    public Guid Id { get; set; }
    public int MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public decimal Value { get; set; }
    public string Value_type { get; set; } = string.Empty;
}
