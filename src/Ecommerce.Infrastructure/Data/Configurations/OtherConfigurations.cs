using Ecommerce.Domain.Entities.Inventory;
using Ecommerce.Domain.Entities.Notification;
using Ecommerce.Domain.Entities.Cms;
using Ecommerce.Domain.Entities.Shipping;
using Ecommerce.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Name).IsRequired().HasMaxLength(200);
        builder.Property(w => w.Code).IsRequired().HasMaxLength(20);
        builder.Property(w => w.Address).IsRequired().HasMaxLength(200);
        builder.Property(w => w.City).IsRequired().HasMaxLength(100);
        builder.Property(w => w.State).HasMaxLength(100);
        builder.Property(w => w.Country).IsRequired().HasMaxLength(2);
        builder.Property(w => w.PostalCode).HasMaxLength(20);
        builder.Property(w => w.Phone).HasMaxLength(20);
        builder.Property(w => w.Email).HasMaxLength(256);
        builder.Property(w => w.ManagerName).HasMaxLength(100);
        builder.HasIndex(w => w.Code).IsUnique();
        builder.HasIndex(w => w.IsActive);
    }
}

public class WarehouseInventoryConfiguration : IEntityTypeConfiguration<WarehouseInventory>
{
    public void Configure(EntityTypeBuilder<WarehouseInventory> builder)
    {
        builder.ToTable("WarehouseInventories");
        builder.HasKey(wi => wi.Id);
        builder.HasIndex(wi => new { wi.WarehouseId, wi.ProductId }).IsUnique();
        builder.HasIndex(wi => new { wi.WarehouseId, wi.ProductId, wi.VariantId });
        builder.HasOne(wi => wi.Warehouse).WithMany(w => w.Inventories).HasForeignKey(wi => wi.WarehouseId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(wi => wi.Product).WithMany().HasForeignKey(wi => wi.ProductId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
        builder.Property(s => s.ContactPerson).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Email).IsRequired().HasMaxLength(256);
        builder.Property(s => s.Phone).HasMaxLength(20);
        builder.Property(s => s.TaxId).HasMaxLength(50);
        builder.HasIndex(s => s.Email);
        builder.HasIndex(s => s.IsActive);
        builder.HasMany(s => s.Products).WithOne(sp => sp.Supplier).HasForeignKey(sp => sp.SupplierId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class SupplierProductConfiguration : IEntityTypeConfiguration<SupplierProduct>
{
    public void Configure(EntityTypeBuilder<SupplierProduct> builder)
    {
        builder.ToTable("SupplierProducts");
        builder.HasKey(sp => sp.Id);
        builder.Property(sp => sp.SupplierSku).HasMaxLength(50);
        builder.Property(sp => sp.CostPrice).HasColumnType("decimal(18,2)");
        builder.HasIndex(sp => new { sp.SupplierId, sp.ProductId }).IsUnique();
    }
}

public class NotificationRecordConfiguration : IEntityTypeConfiguration<NotificationRecord>
{
    public void Configure(EntityTypeBuilder<NotificationRecord> builder)
    {
        builder.ToTable("Notifications");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Type).IsRequired().HasMaxLength(20);
        builder.HasIndex(n => n.UserId);
        builder.HasIndex(n => n.IsRead);
        builder.HasIndex(n => n.CreatedAt);
    }
}

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.ToTable("EmailTemplates");
        builder.HasKey(et => et.Id);
        builder.Property(et => et.Name).IsRequired().HasMaxLength(100);
        builder.Property(et => et.Subject).IsRequired().HasMaxLength(200);
        builder.HasIndex(et => et.Name).IsUnique();
    }
}

public class CmsPageConfiguration : IEntityTypeConfiguration<CmsPage>
{
    public void Configure(EntityTypeBuilder<CmsPage> builder)
    {
        builder.ToTable("CmsPages");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Slug).IsRequired().HasMaxLength(200);
        builder.HasIndex(p => p.Slug).IsUnique();
        builder.HasIndex(p => p.IsPublished);
        builder.HasMany(p => p.Revisions).WithOne(r => r.Page).HasForeignKey(r => r.PageId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class NavigationMenuConfiguration : IEntityTypeConfiguration<NavigationMenu>
{
    public void Configure(EntityTypeBuilder<NavigationMenu> builder)
    {
        builder.ToTable("NavigationMenus");
        builder.HasKey(nm => nm.Id);
        builder.Property(nm => nm.Name).IsRequired().HasMaxLength(100);
        builder.Property(nm => nm.Position).HasMaxLength(50);
        builder.HasMany(nm => nm.Items).WithOne(ni => ni.Menu).HasForeignKey(ni => ni.MenuId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class NavigationMenuItemConfiguration : IEntityTypeConfiguration<NavigationMenuItem>
{
    public void Configure(EntityTypeBuilder<NavigationMenuItem> builder)
    {
        builder.ToTable("NavigationMenuItems");
        builder.HasKey(ni => ni.Id);
        builder.Property(ni => ni.Label).IsRequired().HasMaxLength(100);
        builder.Property(ni => ni.Url).HasMaxLength(500);
        builder.HasIndex(ni => ni.MenuId);
        builder.HasIndex(ni => ni.SortOrder);
    }
}

public class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.ToTable("SiteSettings");
        builder.HasKey(ss => ss.Id);
        builder.Property(ss => ss.Key).IsRequired().HasMaxLength(100);
        builder.Property(ss => ss.Group).HasMaxLength(50);
        builder.HasIndex(ss => ss.Key).IsUnique();
    }
}

public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.ToTable("MediaFiles");
        builder.HasKey(mf => mf.Id);
        builder.Property(mf => mf.FileName).IsRequired().HasMaxLength(256);
        builder.Property(mf => mf.OriginalFileName).IsRequired().HasMaxLength(256);
        builder.Property(mf => mf.Url).IsRequired().HasMaxLength(500);
        builder.Property(mf => mf.ContentType).HasMaxLength(100);
        builder.Property(mf => mf.Folder).HasMaxLength(100);
    }
}

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.TrackingNumber).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Carrier).IsRequired().HasMaxLength(50);
        builder.Property(s => s.ShippingMethod).HasMaxLength(50);
        builder.Property(s => s.ShippingCost).HasColumnType("decimal(18,2)");
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(50);
        builder.HasIndex(s => s.TrackingNumber).IsUnique();
        builder.HasIndex(s => s.OrderId).IsUnique();
    }
}

public class ShippingRateConfiguration : IEntityTypeConfiguration<ShippingRate>
{
    public void Configure(EntityTypeBuilder<ShippingRate> builder)
    {
        builder.ToTable("ShippingRates");
        builder.HasKey(sr => sr.Id);
        builder.Property(sr => sr.Name).IsRequired().HasMaxLength(100);
        builder.Property(sr => sr.Carrier).IsRequired().HasMaxLength(50);
        builder.Property(sr => sr.BaseCost).HasColumnType("decimal(18,2)");
        builder.Property(sr => sr.CostPerKg).HasColumnType("decimal(18,2)");
        builder.Property(sr => sr.FreeShippingThreshold).HasColumnType("decimal(18,2)");
    }
}

public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<OrderIdempotencyRecord>
{
    public void Configure(EntityTypeBuilder<OrderIdempotencyRecord> builder)
    {
        builder.ToTable("IdempotencyRecords");
        builder.HasKey(ir => ir.Id);
        builder.Property(ir => ir.IdempotencyKey).IsRequired().HasMaxLength(100);
        builder.HasIndex(ir => ir.IdempotencyKey).IsUnique();
    }
}
