using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Inventory;

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int CurrentOccupancy { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; }
    public bool SupportsPickup { get; set; }
    public bool SupportsShipping { get; set; }
    public double OccupancyRate => Capacity > 0 ? (double)CurrentOccupancy / Capacity * 100 : 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<WarehouseInventory> Inventories { get; set; } = new List<WarehouseInventory>();
    public virtual ICollection<WarehouseInventoryMovement> Movements { get; set; } = new List<WarehouseInventoryMovement>();
}

public class WarehouseInventory : BaseEntity
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => Quantity - ReservedQuantity;
    public int ReorderLevel { get; set; }
    public int ReorderQuantity { get; set; }
    public bool NeedsReorder => AvailableQuantity <= ReorderLevel;
    public DateTime? LastRestockedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual Catalog.Product Product { get; set; } = null!;
}

public class WarehouseInventoryMovement : BaseEntity
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? FromWarehouseId { get; set; }
    public string MovementType { get; set; } = string.Empty; // "Inbound", "Outbound", "Transfer", "Adjustment"
    public int Quantity { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public string? PerformedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual Catalog.Product Product { get; set; } = null!;
    public virtual Warehouse? FromWarehouse { get; set; }
}

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? TaxId { get; set; }
    public string? Notes { get; set; }
    public decimal Rating { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<SupplierProduct> Products { get; set; } = new List<SupplierProduct>();
}

public class SupplierProduct : BaseEntity
{
    public Guid SupplierId { get; set; }
    public Guid ProductId { get; set; }
    public string? SupplierSku { get; set; }
    public decimal CostPrice { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public int LeadTimeDays { get; set; }
    public bool IsPreferred { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Supplier Supplier { get; set; } = null!;
    public virtual Catalog.Product Product { get; set; } = null!;
}
