namespace Ecommerce.Domain.Enums;

public enum ProductStatus
{
    Draft = 0,
    Active = 1,
    Inactive = 2,
    Archived = 3,
    Discontinued = 4
}

public enum ProductType
{
    Physical = 0,
    Digital = 1,
    Service = 2,
    Subscription = 3,
    Bundle = 4
}

public enum TaxType
{
    None = 0,
    Fixed = 1,
    Percentage = 2
}

public enum WeightUnit
{
    Gram = 0,
    Kilogram = 1,
    Ounce = 2,
    Pound = 3
}

public enum DimensionUnit
{
    Centimeter = 0,
    Meter = 1,
    Inch = 2,
    Foot = 3
}

public enum SortOrder
{
    Ascending = 0,
    Descending = 1
}

public enum ProductSortField
{
    Name = 0,
    Price = 1,
    CreatedAt = 2,
    UpdatedAt = 3,
    Popularity = 4,
    Rating = 5,
    StockQuantity = 6
}
