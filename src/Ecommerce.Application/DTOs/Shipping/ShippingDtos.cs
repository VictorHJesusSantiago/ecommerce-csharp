using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.Application.DTOs.Shipping;

public class ShipmentDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string TrackingNumber { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? ServiceLevel { get; set; }
    public decimal Weight { get; set; }
    public string? WeightUnit { get; set; } = "lbs";
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public string? DimensionUnit { get; set; } = "in";
    public decimal ShippingCost { get; set; }
    public string? ShippingMethod { get; set; }
    public AddressDto? ShippingAddress { get; set; }
    public AddressDto? ReturnAddress { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? EstimatedDelivery { get; set; }
    public DateTime? ActualDelivery { get; set; }
    public string? SignatureRequired { get; set; }
    public string? InsuranceAmount { get; set; }
    public List<ShipmentItemDto> Items { get; set; } = [];
    public List<ShipmentEventDto> Events { get; set; } = [];
    public string? LabelUrl { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ShipmentItemDto
{
    public Guid Id { get; set; }
    public Guid ShipmentId { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; }
}

public class ShipmentEventDto
{
    public Guid Id { get; set; }
    public Guid ShipmentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? CarrierCode { get; set; }
    public DateTime EventTime { get; set; }
    public bool IsException { get; set; }
    public string? ExceptionDescription { get; set; }
}

public class ShippingRateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string ServiceLevel { get; set; } = string.Empty;
    public decimal BaseRate { get; set; }
    public decimal? PerKgRate { get; set; }
    public decimal? PerItemRate { get; set; }
    public decimal? MinimumCharge { get; set; }
    public decimal? MaximumCharge { get; set; }
    public decimal? FreeShippingThreshold { get; set; }
    public TimeSpan? EstimatedTransitTime { get; set; }
    public bool IsActive { get; set; } = true;
    public List<string> SupportedCountries { get; set; } = [];
    public List<string> ExcludedPostalCodes { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CalculateShipmentRequest
{
    public string OriginCountry { get; set; } = string.Empty;
    public string OriginPostalCode { get; set; } = string.Empty;
    public string DestinationCountry { get; set; } = string.Empty;
    public string DestinationPostalCode { get; set; } = string.Empty;
    public decimal TotalWeight { get; set; }
    public string WeightUnit { get; set; } = "lbs";
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public string DimensionUnit { get; set; } = "in";
    public int ItemCount { get; set; }
    public decimal DeclaredValue { get; set; }
    public string? ServiceLevel { get; set; }
    public bool InsuranceRequested { get; set; }
    public bool SignatureRequired { get; set; }
}

public class ShipmentRateResult
{
    public string Carrier { get; set; } = string.Empty;
    public string ServiceLevel { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public decimal? InsuranceCost { get; set; }
    public decimal TotalCost => Rate + (InsuranceCost ?? 0);
    public TimeSpan EstimatedTransitTime { get; set; }
    public DateTime EstimatedDelivery { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? Currency { get; set; } = "USD";
    public List<string> Warnings { get; set; } = [];
    public List<string> Restrictions { get; set; } = [];
}

public class TrackShipmentRequest
{
    public string TrackingNumber { get; set; } = string.Empty;
    public string? Carrier { get; set; }
}

public class TrackShipmentResponse
{
    public string TrackingNumber { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? StatusDescription { get; set; }
    public DateTime? EstimatedDelivery { get; set; }
    public DateTime? ActualDelivery { get; set; }
    public string? SignedBy { get; set; }
    public List<ShipmentEventDto> Events { get; set; } = [];
    public string? TrackingUrl { get; set; }
}

public class CreateShipmentRequest
{
    public Guid OrderId { get; set; }
    public string Carrier { get; set; } = string.Empty;
    public string ServiceLevel { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public string WeightUnit { get; set; } = "lbs";
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public string DimensionUnit { get; set; } = "in";
    public string? Notes { get; set; }
    public bool InsuranceRequested { get; set; }
    public bool SignatureRequired { get; set; }
    public List<CreateShipmentItemRequest> Items { get; set; } = [];
}

public class CreateShipmentItemRequest
{
    public Guid OrderItemId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateShipmentStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public bool IsException { get; set; }
    public string? ExceptionDescription { get; set; }
}

public class AddressDto
{
    public Guid Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string? Street2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string? FullName { get; set; }
}

public class ShippingCarrierDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; }
    public List<string> SupportedServices { get; set; } = [];
    public bool SupportsTracking { get; set; }
    public bool SupportsLabels { get; set; }
    public bool SupportsInsurance { get; set; }
    public bool SupportsPickup { get; set; }
    public decimal MaxWeight { get; set; }
    public string WeightUnit { get; set; } = "lbs";
}

public class ShippingZoneDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> Countries { get; set; } = [];
    public List<string> States { get; set; } = [];
    public List<string> PostalCodePatterns { get; set; } = [];
    public List<ShippingZoneRateDto> Rates { get; set; } = [];
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ShippingZoneRateDto
{
    public Guid Id { get; set; }
    public Guid ZoneId { get; set; }
    public string Carrier { get; set; } = string.Empty;
    public string ServiceLevel { get; set; } = string.Empty;
    public decimal BaseRate { get; set; }
    public decimal? PerKgRate { get; set; }
    public decimal? PerItemRate { get; set; }
    public decimal? FreeShippingThreshold { get; set; }
    public TimeSpan? EstimatedTransitTime { get; set; }
}

public class ShippingAnalyticsDto
{
    public int TotalShipments { get; set; }
    public int DeliveredShipments { get; set; }
    public int InTransitShipments { get; set; }
    public int ExceptionShipments { get; set; }
    public int ReturnedShipments { get; set; }
    public decimal TotalShippingCost { get; set; }
    public decimal AverageShippingCost { get; set; }
    public decimal AverageDeliveryTime { get; set; }
    public decimal OnTimeDeliveryRate { get; set; }
    public decimal DamageRate { get; set; }
    public List<CarrierAnalyticsDto> CarrierAnalytics { get; set; } = [];
    public List<DailyShipmentDto> DailyShipments { get; set; } = [];
}

public class CarrierAnalyticsDto
{
    public string Carrier { get; set; } = string.Empty;
    public int ShipmentCount { get; set; }
    public decimal TotalCost { get; set; }
    public decimal AverageCost { get; set; }
    public decimal OnTimeRate { get; set; }
    public decimal DamageRate { get; set; }
    public decimal AverageDeliveryDays { get; set; }
    public int ExceptionCount { get; set; }
}

public class DailyShipmentDto
{
    public DateTime Date { get; set; }
    public int ShipmentCount { get; set; }
    public int DeliveredCount { get; set; }
    public int ExceptionCount { get; set; }
    public decimal TotalCost { get; set; }
    public decimal AverageCost { get; set; }
}
