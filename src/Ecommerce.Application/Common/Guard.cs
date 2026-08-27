namespace Ecommerce.Application.Common;

public static class Guard
{
    public static void NotNull<T>(T? value, string parameterName) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(parameterName);
    }

    public static void NotNull<T>(T? value, string parameterName) where T : struct
    {
        if (!value.HasValue)
            throw new ArgumentNullException(parameterName);
    }

    public static void NotEmpty(Guid value, string parameterName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"'{parameterName}' must not be empty.", parameterName);
    }

    public static void NotEmpty(Guid? value, string parameterName)
    {
        if (!value.HasValue || value.Value == Guid.Empty)
            throw new ArgumentException($"'{parameterName}' must not be empty.", parameterName);
    }

    public static void NotNullOrWhiteSpace(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"'{parameterName}' must not be empty or whitespace.", parameterName);
    }

    public static void MinLength(string value, int minLength, string parameterName)
    {
        if (value.Length < minLength)
            throw new ArgumentException($"'{parameterName}' must be at least {minLength} characters long.", parameterName);
    }

    public static void MaxLength(string value, int maxLength, string parameterName)
    {
        if (value.Length > maxLength)
            throw new ArgumentException($"'{parameterName}' must not exceed {maxLength} characters.", parameterName);
    }

    public static void GreaterThan<T>(T value, T minValue, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(minValue) <= 0)
            throw new ArgumentException($"'{parameterName}' must be greater than {minValue}.", parameterName);
    }

    public static void GreaterThanOrEqualTo<T>(T value, T minValue, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(minValue) < 0)
            throw new ArgumentException($"'{parameterName}' must be greater than or equal to {minValue}.", parameterName);
    }

    public static void LessThan<T>(T value, T maxValue, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(maxValue) >= 0)
            throw new ArgumentException($"'{parameterName}' must be less than {maxValue}.", parameterName);
    }

    public static void LessThanOrEqualTo<T>(T value, T maxValue, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(maxValue) > 0)
            throw new ArgumentException($"'{parameterName}' must be less than or equal to {maxValue}.", parameterName);
    }

    public static void InRange<T>(T value, T min, T max, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentException($"'{parameterName}' must be between {min} and {max}.", parameterName);
    }

    public static void IsValidEnum<TEnum>(TEnum value, string parameterName) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
            throw new ArgumentException($"'{parameterName}' is not a valid value for {typeof(TEnum).Name}.", parameterName);
    }

    public static void InvalidEnum<TEnum>(TEnum value, string parameterName) where TEnum : struct, Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            throw new ArgumentException($"'{parameterName}' should not be a valid enum value.", parameterName);
    }

    public static void Requires(bool condition, string message)
    {
        if (!condition)
            throw new InvalidOperationException(message);
    }

    public static void NotDefault<T>(T value, string parameterName) where T : struct
    {
        if (value.Equals(default(T)))
            throw new ArgumentException($"'{parameterName}' must not be default value.", parameterName);
    }

    public static void Contains<T>(IEnumerable<T> collection, T item, string parameterName) where T : notnull
    {
        if (!collection.Contains(item))
            throw new ArgumentException($"'{parameterName}' must contain the specified item.", parameterName);
    }

    public static void NotEmpty<T>(IEnumerable<T> collection, string parameterName)
    {
        if (!collection.Any())
            throw new ArgumentException($"'{parameterName}' must not be empty.", parameterName);
    }
}

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.") { }
}

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message = "You do not have permission to perform this action.")
        : base(message) { }
}

public class ValidationAppException : DomainException
{
    public IEnumerable<string> Errors { get; }

    public ValidationAppException(IEnumerable<string> errors)
        : base("Validation failed.")
    {
        Errors = errors;
    }
}
