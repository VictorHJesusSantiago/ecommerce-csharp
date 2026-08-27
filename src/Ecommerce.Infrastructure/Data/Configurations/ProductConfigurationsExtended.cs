namespace Ecommerce.Infrastructure.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Url).IsRequired().HasMaxLength(500);
        builder.Property(e => e.AltText).HasMaxLength(200);
        builder.HasOne(e => e.Product).WithMany(p => p.Images).HasForeignKey(e => e.ProductId);
    }
}

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Sku).HasMaxLength(100);
        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
        builder.HasOne(e => e.Product).WithMany(p => p.Variants).HasForeignKey(e => e.ProductId);
    }
}

public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Slug).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => e.Slug).IsUnique();
    }
}

public class ProductCollectionConfiguration : IEntityTypeConfiguration<ProductCollection>
{
    public void Configure(EntityTypeBuilder<ProductCollection> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Slug).HasMaxLength(200);
    }
}

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.MovementType).IsRequired().HasMaxLength(50);
        builder.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId);
    }
}

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(e => new { e.ProductId, e.CategoryId });
        builder.HasOne(e => e.Product).WithMany(p => p.ProductCategories).HasForeignKey(e => e.ProductId);
        builder.HasOne(e => e.Category).WithMany(c => c.ProductCategories).HasForeignKey(e => e.CategoryId);
    }
}

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Rating).IsRequired();
        builder.Property(e => e.Title).HasMaxLength(200);
        builder.Property(e => e.Comment).HasMaxLength(5000);
        builder.HasOne(e => e.Product).WithMany(p => p.Reviews).HasForeignKey(e => e.ProductId);
    }
}

public class ReviewImageConfiguration : IEntityTypeConfiguration<ReviewImage>
{
    public void Configure(EntityTypeBuilder<ReviewImage> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Url).IsRequired().HasMaxLength(500);
        builder.HasOne(e => e.Review).WithMany(r => r.Images).HasForeignKey(e => e.ReviewId);
    }
}

public class ReviewHelpfulnessVoteConfiguration : IEntityTypeConfiguration<ReviewHelpfulnessVote>
{
    public void Configure(EntityTypeBuilder<ReviewHelpfulnessVote> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Review).WithMany().HasForeignKey(e => e.ReviewId);
    }
}

public class ReviewReportConfiguration : IEntityTypeConfiguration<ReviewReport>
{
    public void Configure(EntityTypeBuilder<ReviewReport> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Reason).IsRequired().HasMaxLength(200);
        builder.HasOne(e => e.Review).WithMany().HasForeignKey(e => e.ReviewId);
    }
}
