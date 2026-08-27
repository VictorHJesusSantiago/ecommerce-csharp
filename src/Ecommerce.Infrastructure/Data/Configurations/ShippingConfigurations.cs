namespace Ecommerce.Infrastructure.Data.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.TrackingNumber).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Carrier).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Status).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ShippingCost).HasColumnType("decimal(18,2)");
        builder.HasOne(e => e.Order).WithMany(o => o.Shipments).HasForeignKey(e => e.OrderId);
    }
}

public class ShipmentItemConfiguration : IEntityTypeConfiguration<ShipmentItem>
{
    public void Configure(EntityTypeBuilder<ShipmentItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Quantity).IsRequired();
        builder.HasOne(e => e.Shipment).WithMany(s => s.Items).HasForeignKey(e => e.ShipmentId);
    }
}

public class ShipmentEventConfiguration : IEntityTypeConfiguration<ShipmentEvent>
{
    public void Configure(EntityTypeBuilder<ShipmentEvent> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Status).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.HasOne(e => e.Shipment).WithMany(s => s.Events).HasForeignKey(e => e.ShipmentId);
    }
}

public class ShippingRateConfiguration : IEntityTypeConfiguration<ShippingRate>
{
    public void Configure(EntityTypeBuilder<ShippingRate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Carrier).IsRequired().HasMaxLength(50);
        builder.Property(e => e.BaseRate).HasColumnType("decimal(18,2)");
        builder.Property(e => e.PerKgRate).HasColumnType("decimal(18,2)");
        builder.Property(e => e.FreeShippingThreshold).HasColumnType("decimal(18,2)");
    }
}

public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<IdempotencyRecord>
{
    public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Key).IsRequired().HasMaxLength(200);
        builder.HasIndex(e => e.Key).IsUnique();
    }
}
