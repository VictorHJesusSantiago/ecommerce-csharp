using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Slug).IsRequired().HasMaxLength(200);
        builder.Property(p => p.SKU).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Barcode).HasMaxLength(50);
        builder.Property(p => p.Price).HasPrecision(18, 2);
        builder.Property(p => p.CompareAtPrice).HasPrecision(18, 2);
        builder.Property(p => p.CostPrice).HasPrecision(18, 2);
        builder.Property(p => p.Currency).HasMaxLength(3);
        builder.Property(p => p.TaxRate).HasPrecision(5, 2);
        builder.Property(p => p.Weight).HasPrecision(10, 2);
        builder.Property(p => p.WeightUnit).HasMaxLength(10);
        builder.Property(p => p.Length).HasPrecision(10, 2);
        builder.Property(p => p.Width).HasPrecision(10, 2);
        builder.Property(p => p.Height).HasPrecision(10, 2);
        builder.Property(p => p.DimensionUnit).HasMaxLength(10);
        builder.Property(p => p.MetaTitle).HasMaxLength(200);
        builder.Property(p => p.MetaDescription).HasMaxLength(500);
        builder.Property(p => p.Tags).HasMaxLength(1000);
        builder.Property(p => p.AverageRating).HasPrecision(3, 2);
        builder.HasIndex(p => p.Slug).IsUnique();
        builder.HasIndex(p => p.SKU).IsUnique();
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.BrandId);
        builder.HasIndex(p => p.IsFeatured);
        builder.HasIndex(p => p.CreatedAt);
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("ProductVariants");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Name).IsRequired().HasMaxLength(200);
        builder.Property(v => v.SKU).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Price).HasPrecision(18, 2);
        builder.Property(v => v.CompareAtPrice).HasPrecision(18, 2);
        builder.Property(v => v.CostPrice).HasPrecision(18, 2);
        builder.Property(v => v.Weight).HasPrecision(10, 2);
        builder.Property(v => v.Option1).HasMaxLength(100);
        builder.Property(v => v.Option2).HasMaxLength(100);
        builder.Property(v => v.Option3).HasMaxLength(100);
        builder.HasIndex(v => v.SKU);
        builder.HasIndex(v => new { v.ProductId, v.SKU });
    }
}

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Url).IsRequired().HasMaxLength(2048);
        builder.Property(i => i.AltText).HasMaxLength(200);
        builder.Property(i => i.Title).HasMaxLength(200);
        builder.Property(i => i.FileName).HasMaxLength(255);
        builder.Property(i => i.ContentType).HasMaxLength(100);
        builder.HasIndex(i => new { i.ProductId, i.DisplayOrder });
    }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Slug).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(2000);
        builder.Property(c => c.ImageUrl).HasMaxLength(2048);
        builder.Property(c => c.BannerUrl).HasMaxLength(2048);
        builder.Property(c => c.MetaTitle).HasMaxLength(200);
        builder.Property(c => c.MetaDescription).HasMaxLength(500);
        builder.Property(c => c.MetaKeywords).HasMaxLength(500);
        builder.HasIndex(c => c.Slug).IsUnique();
        builder.HasIndex(c => c.ParentCategoryId);
        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Slug).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Description).HasMaxLength(2000);
        builder.Property(b => b.LogoUrl).HasMaxLength(2048);
        builder.Property(b => b.BannerUrl).HasMaxLength(2048);
        builder.Property(b => b.WebsiteUrl).HasMaxLength(2048);
        builder.Property(b => b.MetaTitle).HasMaxLength(200);
        builder.Property(b => b.MetaDescription).HasMaxLength(500);
        builder.HasIndex(b => b.Slug).IsUnique();
        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}
