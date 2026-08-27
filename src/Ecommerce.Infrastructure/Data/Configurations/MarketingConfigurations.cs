using Ecommerce.Domain.Entities.Marketing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
        builder.Property(c => c.DiscountValue).HasColumnType("decimal(18,2)");
        builder.Property(c => c.MinimumOrderAmount).HasColumnType("decimal(18,2)");
        builder.Property(c => c.MaximumDiscountAmount).HasColumnType("decimal(18,2)");
        builder.HasIndex(c => c.Code).IsUnique();
        builder.HasIndex(c => c.IsActive);
        builder.HasIndex(c => c.StartDate);
        builder.HasIndex(c => c.EndDate);
        builder.HasMany(c => c.Usages).WithOne(u => u.Coupon).HasForeignKey(u => u.CouponId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("Banners");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.ImageUrl).IsRequired().HasMaxLength(500);
        builder.Property(b => b.LinkUrl).HasMaxLength(500);
        builder.Property(b => b.TargetUrl).HasMaxLength(500);
        builder.Property(b => b.Position).HasConversion<string>().HasMaxLength(50);
        builder.HasIndex(b => b.Position);
        builder.HasIndex(b => b.IsActive);
        builder.HasIndex(b => b.SortOrder);
    }
}

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotions");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.DiscountValue).HasColumnType("decimal(18,2)");
        builder.Property(p => p.MinimumOrderAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.CouponCode).HasMaxLength(50);
        builder.HasIndex(p => p.IsActive);
        builder.HasIndex(p => p.StartDate);
        builder.HasIndex(p => p.EndDate);
    }
}

public class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.ToTable("NewsletterSubscribers");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Email).IsRequired().HasMaxLength(256);
        builder.HasIndex(n => n.Email).IsUnique();
        builder.HasIndex(n => n.IsActive);
    }
}

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.Property(d => d.Value).HasColumnType("decimal(18,2)");
        builder.Property(d => d.MinOrderAmount).HasColumnType("decimal(18,2)");
        builder.HasMany(d => d.Tiers).WithOne(t => t.Discount).HasForeignKey(t => t.DiscountId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class DiscountTierConfiguration : IEntityTypeConfiguration<DiscountTier>
{
    public void Configure(EntityTypeBuilder<DiscountTier> builder)
    {
        builder.ToTable("DiscountTiers");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Value).HasColumnType("decimal(18,2)");
    }
}
