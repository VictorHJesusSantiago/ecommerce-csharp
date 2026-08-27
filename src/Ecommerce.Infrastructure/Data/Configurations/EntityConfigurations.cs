using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Domain.Entities.Cart;
using Ecommerce.Domain.Entities.Payment;
using Ecommerce.Domain.Entities.Review;
using Ecommerce.Domain.Entities.Shipping;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
        builder.Property(o => o.Currency).HasMaxLength(3);
        builder.Property(o => o.SubTotal).HasPrecision(18, 2);
        builder.Property(o => o.DiscountAmount).HasPrecision(18, 2);
        builder.Property(o => o.ShippingAmount).HasPrecision(18, 2);
        builder.Property(o => o.TaxAmount).HasPrecision(18, 2);
        builder.Property(o => o.GrandTotal).HasPrecision(18, 2);
        builder.Property(o => o.RefundAmount).HasPrecision(18, 2);
        builder.Property(o => o.CouponCode).HasMaxLength(50);
        builder.Property(o => o.CouponDiscount).HasPrecision(18, 2);
        builder.Property(o => o.IpAddress).HasMaxLength(45);
        builder.Property(o => o.Source).HasMaxLength(50);
        builder.Property(o => o.CancellationReason).HasMaxLength(500);
        builder.Property(o => o.WalletAmountUsed).HasPrecision(18, 2);
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.HasIndex(o => o.CustomerId);
        builder.HasIndex(o => o.Status);
        builder.HasIndex(o => o.CreatedAt);
        builder.HasQueryFilter(o => !o.IsDeleted);

        builder.HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.History)
            .WithOne(h => h.Order)
            .HasForeignKey(h => h.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Notes2)
            .WithOne(n => n.Order)
            .HasForeignKey(n => n.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ProductName).IsRequired().HasMaxLength(500);
        builder.Property(i => i.ProductSlug).HasMaxLength(500);
        builder.Property(i => i.ProductImageUrl).HasMaxLength(2048);
        builder.Property(i => i.SKU).IsRequired().HasMaxLength(50);
        builder.Property(i => i.VariantName).HasMaxLength(200);
        builder.Property(i => i.UnitPrice).HasPrecision(18, 2);
        builder.Property(i => i.DiscountAmount).HasPrecision(18, 2);
        builder.Property(i => i.TaxAmount).HasPrecision(18, 2);
        builder.Property(i => i.LineTotal).HasPrecision(18, 2);
        builder.Property(i => i.GiftMessage).HasMaxLength(500);
    }
}

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUsers");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).IsRequired().HasMaxLength(256);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(100);
        builder.Property(u => u.LastName).HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);
        builder.Property(u => u.AvatarUrl).HasMaxLength(2048);
        builder.Property(u => u.Company).HasMaxLength(200);
        builder.Property(u => u.JobTitle).HasMaxLength(200);
        builder.Property(u => u.SecurityStamp).HasMaxLength(256);
        builder.Property(u => u.ConcurrencyStamp).HasMaxLength(256);
        builder.Property(u => u.LastLoginIp).HasMaxLength(45);
        builder.Property(u => u.RegistrationIp).HasMaxLength(45);
        builder.Property(u => u.ReferralCode).HasMaxLength(50);
        builder.Property(u => u.WalletBalance).HasPrecision(18, 2);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail);
        builder.HasIndex(u => u.PhoneNumber);
        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.SessionId).HasMaxLength(100);
        builder.Property(c => c.CouponCode).HasMaxLength(50);
        builder.Property(c => c.CouponDiscount).HasPrecision(18, 2);
        builder.Property(c => c.Currency).HasMaxLength(3);
        builder.Property(c => c.Notes).HasMaxLength(1000);
        builder.Property(c => c.IpAddress).HasMaxLength(45);
        builder.HasIndex(c => c.UserId);
        builder.HasIndex(c => c.SessionId);

        builder.HasMany(c => c.Items)
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ProductName).IsRequired().HasMaxLength(500);
        builder.Property(i => i.ProductSlug).HasMaxLength(500);
        builder.Property(i => i.ProductImageUrl).HasMaxLength(2048);
        builder.Property(i => i.UnitPrice).HasPrecision(18, 2);
        builder.Property(i => i.CompareAtPrice).HasPrecision(18, 2);
        builder.Property(i => i.VariantName).HasMaxLength(200);
        builder.Property(i => i.SKU).HasMaxLength(50);
        builder.Property(i => i.Options).HasMaxLength(1000);
        builder.Property(i => i.Weight).HasPrecision(10, 2);
    }
}

public class PaymentRecordConfiguration : IEntityTypeConfiguration<PaymentRecord>
{
    public void Configure(EntityTypeBuilder<PaymentRecord> builder)
    {
        builder.ToTable("PaymentRecords");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.TransactionId).IsRequired().HasMaxLength(100);
        builder.Property(p => p.GatewayTransactionId).HasMaxLength(200);
        builder.Property(p => p.Amount).HasPrecision(18, 2);
        builder.Property(p => p.ProcessedAmount).HasPrecision(18, 2);
        builder.Property(p => p.Currency).HasMaxLength(3);
        builder.Property(p => p.ExchangeRate).HasPrecision(18, 6);
        builder.Property(p => p.ConvertedAmount).HasPrecision(18, 2);
        builder.Property(p => p.CardLast4).HasMaxLength(4);
        builder.Property(p => p.CardBrand).HasMaxLength(50);
        builder.Property(p => p.CardHolderName).HasMaxLength(200);
        builder.Property(p => p.BillingEmail).HasMaxLength(256);
        builder.Property(p => p.BillingName).HasMaxLength(200);
        builder.Property(p => p.FailureReason).HasMaxLength(500);
        builder.Property(p => p.FailureCode).HasMaxLength(100);
        builder.Property(p => p.IpAddress).HasMaxLength(45);
        builder.HasIndex(p => p.TransactionId);
        builder.HasIndex(p => p.OrderId);

        builder.HasMany(p => p.Refunds)
            .WithOne(r => r.Payment)
            .HasForeignKey(r => r.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class RefundRecordConfiguration : IEntityTypeConfiguration<RefundRecord>
{
    public void Configure(EntityTypeBuilder<RefundRecord> builder)
    {
        builder.ToTable("RefundRecords");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RefundTransactionId).IsRequired().HasMaxLength(100);
        builder.Property(r => r.GatewayRefundId).HasMaxLength(200);
        builder.Property(r => r.Amount).HasPrecision(18, 2);
        builder.Property(r => r.Currency).HasMaxLength(3);
        builder.Property(r => r.Notes).HasMaxLength(1000);
        builder.Property(r => r.AdminNotes).HasMaxLength(1000);
        builder.Property(r => r.FailureReason).HasMaxLength(500);
        builder.Property(r => r.RefundMethod).HasMaxLength(50);
        builder.HasIndex(r => r.RefundTransactionId);
    }
}

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.TrackingNumber).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Carrier).HasMaxLength(100);
        builder.Property(s => s.CarrierService).HasMaxLength(100);
        builder.Property(s => s.ShippingCost).HasPrecision(18, 2);
        builder.Property(s => s.EstimatedDeliveryDate).HasMaxLength(50);
        builder.Property(s => s.SignatureRequired).HasMaxLength(10);
        builder.Property(s => s.SignatureName).HasMaxLength(200);
        builder.Property(s => s.ProofOfDeliveryUrl).HasMaxLength(2048);
        builder.Property(s => s.LabelUrl).HasMaxLength(2048);
        builder.HasIndex(s => s.TrackingNumber);
        builder.HasIndex(s => s.OrderId);

        builder.HasMany(s => s.Items)
            .WithOne(i => i.Shipment)
            .HasForeignKey(i => i.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Events)
            .WithOne(e => e.Shipment)
            .HasForeignKey(e => e.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.ToTable("ProductReviews");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Title).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Body).IsRequired().HasMaxLength(5000);
        builder.Property(r => r.Pros).HasMaxLength(2000);
        builder.Property(r => r.Cons).HasMaxLength(2000);
        builder.Property(r => r.AdminResponse).HasMaxLength(5000);
        builder.Property(r => r.ResponseAuthor).HasMaxLength(200);
        builder.HasIndex(r => new { r.ProductId, r.UserId });
        builder.HasIndex(r => r.Status);
        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}

public class CouponConfiguration : IEntityTypeConfiguration<Domain.Entities.Marketing.Coupon>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Marketing.Coupon> builder)
    {
        builder.ToTable("Coupons");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Description).HasMaxLength(500);
        builder.Property(c => c.Value).HasPrecision(18, 2);
        builder.Property(c => c.MinimumOrderAmount).HasPrecision(18, 2);
        builder.Property(c => c.MaximumDiscountAmount).HasPrecision(18, 2);
        builder.HasIndex(c => c.Code).IsUnique();
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}

public class BannerConfiguration : IEntityTypeConfiguration<Domain.Entities.Marketing.Banner>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Marketing.Banner> builder)
    {
        builder.ToTable("Banners");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Subtitle).HasMaxLength(500);
        builder.Property(b => b.ImageUrl).IsRequired().HasMaxLength(2048);
        builder.Property(b => b.MobileImageUrl).HasMaxLength(2048);
        builder.Property(b => b.LinkUrl).HasMaxLength(2048);
        builder.Property(b => b.AltText).HasMaxLength(200);
        builder.HasIndex(b => new { b.Position, b.DisplayOrder });
    }
}

public class WarehouseConfiguration : IEntityTypeConfiguration<Domain.Entities.Inventory.Warehouse>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Inventory.Warehouse> builder)
    {
        builder.ToTable("Warehouses");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Name).IsRequired().HasMaxLength(200);
        builder.Property(w => w.Code).IsRequired().HasMaxLength(50);
        builder.Property(w => w.Description).HasMaxLength(1000);
        builder.Property(w => w.Street).IsRequired().HasMaxLength(500);
        builder.Property(w => w.Street2).HasMaxLength(500);
        builder.Property(w => w.City).IsRequired().HasMaxLength(100);
        builder.Property(w => w.State).IsRequired().HasMaxLength(100);
        builder.Property(w => w.PostalCode).IsRequired().HasMaxLength(20);
        builder.Property(w => w.Country).IsRequired().HasMaxLength(100);
        builder.Property(w => w.ContactName).HasMaxLength(200);
        builder.Property(w => w.ContactPhone).HasMaxLength(20);
        builder.Property(w => w.ContactEmail).HasMaxLength(256);
        builder.HasIndex(w => w.Code).IsUnique();
        builder.HasQueryFilter(w => !w.IsDeleted);
    }
}
