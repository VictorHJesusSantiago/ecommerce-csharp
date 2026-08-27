namespace Ecommerce.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency = "USD")
    {
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add money with different currencies: {Currency} and {other.Currency}");
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract money with different currencies: {Currency} and {other.Currency}");
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(int quantity) => new(Amount * quantity, Currency);
    public Money Multiply(decimal factor) => new(Amount * factor, Currency);
    public bool IsZero => Amount == 0;
    public bool IsPositive => Amount > 0;
    public bool IsNegative => Amount < 0;

    public override string ToString() => $"{Currency} {Amount:N2}";

    public static Money Zero(string currency = "USD") => new(0, currency);
    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator -(Money left, Money right) => left.Subtract(right);
    public static Money operator *(Money money, int quantity) => money.Multiply(quantity);
    public static Money operator *(Money money, decimal factor) => money.Multiply(factor);
}

public record EmailAddress
{
    public string Value { get; init; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email address cannot be empty.", nameof(value));
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email address format.", nameof(value));
        Value = value.ToLowerInvariant();
    }

    public override string ToString() => Value;
}

public record PhoneNumber
{
    public string Value { get; init; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty.", nameof(value));
        Value = value;
    }

    public override string ToString() => Value;
}

public record Address
{
    public string Street { get; init; }
    public string? Street2 { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }
    public string? PhoneNumber { get; init; }

    public Address(string street, string city, string state, string postalCode, string country, string? street2 = null, string? phoneNumber = null)
    {
        Street = street;
        Street2 = street2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        PhoneNumber = phoneNumber;
    }

    public string FullAddress => $"{Street}, {Street2}, {City}, {State} {PostalCode}, {Country}".Replace(", ,", ",");
}

public record Slug
{
    public string Value { get; init; }

    public Slug(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Slug cannot be empty.", nameof(value));
        Value = value.ToLowerInvariant().Replace(" ", "-");
    }

    public override string ToString() => Value;
}

public record Sku
{
    public string Value { get; init; }

    public Sku(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SKU cannot be empty.", nameof(value));
        Value = value.ToUpperInvariant();
    }

    public override string ToString() => Value;
}

public record Barcode
{
    public string Value { get; init; }

    public Barcode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Barcode cannot be empty.", nameof(value));
        Value = value;
    }

    public override string ToString() => Value;
}

public record Percentage
{
    public decimal Value { get; init; }

    public Percentage(decimal value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("Percentage must be between 0 and 100.", nameof(value));
        Value = value;
    }

    public decimal ToDecimal() => Value / 100;
    public override string ToString() => $"{Value}%";
}

public record DateRange
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public DateRange(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
            throw new ArgumentException("End date must be after start date.");
        StartDate = startDate;
        EndDate = endDate;
    }

    public bool Contains(DateTime date) => date >= StartDate && date <= EndDate;
    public int TotalDays => (EndDate - StartDate).Days;
    public bool IsActive => Contains(DateTime.UtcNow);
}

public record MoneyRange
{
    public Money Min { get; init; }
    public Money Max { get; init; }

    public MoneyRange(Money min, Money max)
    {
        if (min.Currency != max.Currency)
            throw new ArgumentException("Currencies must match.");
        if (min.Amount > max.Amount)
            throw new ArgumentException("Min must be less than or equal to Max.");
        Min = min;
        Max = max;
    }

    public bool Contains(Money value) => value.Amount >= Min.Amount && value.Amount <= Max.Amount;
}
