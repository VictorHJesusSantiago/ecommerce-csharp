namespace Ecommerce.Application.DTOs.Inventory;

public class WarehouseDto
{
    public Guid Id { get; set; }
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
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public bool SupportsPickup { get; set; }
    public bool SupportsShipping { get; set; }
    public double OccupancyRate => Capacity > 0 ? (double)CurrentOccupancy / Capacity * 100 : 0;
}

public class InventoryItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => Quantity - ReservedQuantity;
    public int ReorderLevel { get; set; }
    public int ReorderQuantity { get; set; }
    public bool NeedsReorder => AvailableQuantity <= ReorderLevel;
    public DateTime LastRestockedAt { get; set; }
}

public class WarehouseInventoryDto
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public int TotalProducts { get; set; }
    public int TotalQuantity { get; set; }
    public int LowStockCount { get; set; }
    public List<InventoryItemDto> Items { get; set; } = [];
}

public class SupplierDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? TaxId { get; set; }
    public decimal Rating { get; set; }
    public bool IsActive { get; set; }
    public int ProductCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AdjustStockRequest
{
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } = string.Empty; // "Add", "Remove", "Set"
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}

public class TransferStockRequest
{
    public Guid ProductId { get; set; }
    public Guid FromWarehouseId { get; set; }
    public Guid ToWarehouseId { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
}

public class InventoryMovementDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public Guid? FromWarehouseId { get; set; }
    public string? FromWarehouseName { get; set; }
    public Guid? ToWarehouseId { get; set; }
    public string? ToWarehouseName { get; set; }
    public string MovementType { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public string? PerformedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
