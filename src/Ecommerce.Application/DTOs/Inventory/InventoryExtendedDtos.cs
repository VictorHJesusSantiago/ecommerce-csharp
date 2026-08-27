namespace Ecommerce.Application.DTOs.Inventory;

public class InventoryExtendedDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int TotalQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => TotalQuantity - ReservedQuantity;
    public int LowStockThreshold { get; set; }
    public bool IsLowStock => AvailableQuantity <= LowStockThreshold;
    public bool IsOutOfStock => AvailableQuantity <= 0;
    public decimal TotalValue { get; set; }
    public decimal AverageCost { get; set; }
    public decimal TotalCost { get; set; }
    public int TotalMovements { get; set; }
    public DateTime? LastRestockedAt { get; set; }
    public DateTime? LastSoldAt { get; set; }
    public int DaysSinceLastSale { get; set; }
    public decimal AverageDailySales { get; set; }
    public int DaysOfSupply { get; set; }
    public bool RequiresReorder { get; set; }
    public int RecommendedOrderQuantity { get; set; }
    public List<WarehouseInventoryDetailDto> WarehouseBreakdown { get; set; } = [];
    public List<StockMovementDto> RecentMovements { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class WarehouseInventoryDetailDto
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string WarehouseCode { get; set; } = string.Empty;
    public string? WarehouseAddress { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => Quantity - ReservedQuantity;
    public decimal UnitCost { get; set; }
    public decimal TotalValue => UnitCost * Quantity;
    public DateTime? LastRestockedAt { get; set; }
    public DateTime? LastSoldAt { get; set; }
    public bool IsLowStock { get; set; }
    public bool IsOutOfStock { get; set; }
}

public class StockMovementDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public Guid? SourceWarehouseId { get; set; }
    public string? SourceWarehouseName { get; set; }
    public Guid? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public int Quantity { get; set; }
    public string MovementType { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public string? ReferenceId { get; set; }
    public string? Notes { get; set; }
    public decimal? UnitCost { get; set; }
    public decimal? TotalCost => UnitCost * Quantity;
    public string? PerformedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class InventoryAuditDto
{
    public Guid Id { get; set; }
    public string AuditNumber { get; set; } = string.Empty;
    public Guid? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public string Status { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public int MatchedItems { get; set; }
    public int DiscrepancyItems { get; set; }
    public decimal AccuracyPercentage { get; set; }
    public decimal TotalDiscrepancyValue { get; set; }
    public string? StartedBy { get; set; }
    public string? CompletedBy { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public List<InventoryAuditItemDto> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class InventoryAuditItemDto
{
    public Guid Id { get; set; }
    public Guid AuditId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int SystemQuantity { get; set; }
    public int CountedQuantity { get; set; }
    public int Discrepancy => CountedQuantity - SystemQuantity;
    public decimal UnitCost { get; set; }
    public decimal DiscrepancyValue => Math.Abs(Discrepancy) * UnitCost;
    public string? Notes { get; set; }
    public string? CountedBy { get; set; }
    public DateTime? CountedAt { get; set; }
}

public class PurchaseOrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PaymentTerms { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public string? Notes { get; set; }
    public List<PurchaseOrderItemDto> Items { get; set; } = [];
    public List<PurchaseOrderHistoryDto> History { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class PurchaseOrderItemDto
{
    public Guid Id { get; set; }
    public Guid PurchaseOrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public string? SupplierSku { get; set; }
    public int OrderedQuantity { get; set; }
    public int ReceivedQuantity { get; set; }
    public int PendingQuantity => OrderedQuantity - ReceivedQuantity;
    public decimal UnitCost { get; set; }
    public decimal TotalCost => UnitCost * OrderedQuantity;
    public decimal? Discount { get; set; }
    public string? Notes { get; set; }
}

public class PurchaseOrderHistoryDto
{
    public Guid Id { get; set; }
    public Guid PurchaseOrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePurchaseOrderRequest
{
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public string? Notes { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string? PaymentTerms { get; set; }
    public List<CreatePurchaseOrderItemRequest> Items { get; set; } = [];
}

public class CreatePurchaseOrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal? Discount { get; set; }
    public string? Notes { get; set; }
}

public class ReceivePurchaseOrderRequest
{
    public Guid PurchaseOrderId { get; set; }
    public List<ReceivePurchaseOrderItemRequest> Items { get; set; } = [];
    public string? Notes { get; set; }
}

public class ReceivePurchaseOrderItemRequest
{
    public Guid PurchaseOrderItemId { get; set; }
    public int ReceivedQuantity { get; set; }
    public int? DamagedQuantity { get; set; }
    public string? Notes { get; set; }
}

public class InventoryAlertDto
{
    public Guid Id { get; set; }
    public string AlertType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public Guid? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Sku { get; set; }
    public Guid? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public int CurrentQuantity { get; set; }
    public int ThresholdQuantity { get; set; }
    public string? Message { get; set; }
    public bool IsAcknowledged { get; set; }
    public DateTime? AcknowledgedAt { get; set; }
    public string? AcknowledgedBy { get; set; }
    public bool IsResolved { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public string? ResolvedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class InventoryForecastDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int CurrentStock { get; set; }
    public decimal AverageDailySales { get; set; }
    public decimal SalesTrend { get; set; }
    public int DaysOfSupply { get; set; }
    public DateTime? StockoutDate { get; set; }
    public int ReorderPoint { get; set; }
    public int RecommendedOrderQuantity { get; set; }
    public bool RequiresReorder { get; set; }
    public int LeadTimeDays { get; set; }
    public decimal? SafetyStock { get; set; }
    public List<ForecastPeriodDto> Forecast { get; set; } = [];
}

public class ForecastPeriodDto
{
    public DateTime Date { get; set; }
    public decimal PredictedDemand { get; set; }
    public decimal LowerBound { get; set; }
    public decimal UpperBound { get; set; }
    public decimal ProjectedStock { get; set; }
    public bool IsProjectedLowStock { get; set; }
    public bool IsProjectedStockout { get; set; }
}
