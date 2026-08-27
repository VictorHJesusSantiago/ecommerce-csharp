namespace Ecommerce.Application.Common;

public record Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }
    public List<string> Errors { get; init; } = [];
    public int? StatusCode { get; init; }

    public static Result<T> Success(T data, string message = "Operation completed successfully.")
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            StatusCode = 200
        };
    }

    public static Result<T> Failure(string error, int statusCode = 400)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = error,
            StatusCode = statusCode
        };
    }

    public static Result<T> Failure(List<string> errors, int statusCode = 400)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = errors.FirstOrDefault(),
            Errors = errors,
            StatusCode = statusCode
        };
    }

    public static Result<T> NotFound(string message = "Resource not found.")
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = message,
            StatusCode = 404
        };
    }

    public static Result<T> Unauthorized(string message = "Unauthorized.")
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = message,
            StatusCode = 401
        };
    }

    public static Result<T> Forbidden(string message = "Forbidden.")
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = message,
            StatusCode = 403
        };
    }

    public Result<TOut> Map<TOut>(Func<T, TOut> map)
    {
        if (IsSuccess && Data is not null)
            return Result<TOut>.Success(map(Data), Error ?? "Success");
        return Result<TOut>.Failure(Error ?? "Unknown error", StatusCode ?? 500);
    }
}

public record Result
{
    public bool IsSuccess { get; init; }
    public string? Error { get; init; }
    public List<string> Errors { get; init; } = [];
    public int? StatusCode { get; init; }

    public static Result Success(string message = "Operation completed successfully.")
    {
        return new Result { IsSuccess = true, StatusCode = 200 };
    }

    public static Result Failure(string error, int statusCode = 400)
    {
        return new Result { IsSuccess = false, Error = error, StatusCode = statusCode };
    }

    public static Result Failure(List<string> errors, int statusCode = 400)
    {
        return new Result { IsSuccess = false, Error = errors.FirstOrDefault(), Errors = errors, StatusCode = statusCode };
    }

    public static Result NotFound(string message = "Resource not found.")
    {
        return new Result { IsSuccess = false, Error = message, StatusCode = 404 };
    }
}

public class PaginatedList<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public static PaginatedList<T> Create(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
    {
        return new PaginatedList<T>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}

public class Guard
{
    public static void AgainstNegative(decimal value, string parameterName)
    {
        if (value < 0)
            throw new ArgumentException($"{parameterName} cannot be negative.", parameterName);
    }

    public static void AgainstNegativeOrZero(decimal value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentException($"{parameterName} must be greater than zero.", parameterName);
    }

    public static void AgainstNegative(int value, string parameterName)
    {
        if (value < 0)
            throw new ArgumentException($"{parameterName} cannot be negative.", parameterName);
    }

    public static void AgainstNegativeOrZero(int value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentException($"{parameterName} must be greater than zero.", parameterName);
    }

    public static void AgainstEmpty(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{parameterName} cannot be empty.", parameterName);
    }

    public static void AgainstMaxLength(string value, int maxLength, string parameterName)
    {
        if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            throw new ArgumentException($"{parameterName} cannot exceed {maxLength} characters.", parameterName);
    }

    public static void AgainstNullOrDefault<T>(T? value, string parameterName) where T : struct
    {
        if (!value.HasValue)
            throw new ArgumentNullException(parameterName);
    }

    public static void AgainstNull<T>(T? value, string parameterName) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(parameterName);
    }

    public static void AgainstInvalidEmail(string email, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.IsValidEmail())
            throw new ArgumentException($"{parameterName} is not a valid email address.", parameterName);
    }

    public static void AgainstOutOfRange(int value, int min, int max, string parameterName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} must be between {min} and {max}.");
    }

    public static void AgainstOutOfRange(decimal value, decimal min, decimal max, string parameterName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} must be between {min} and {max}.");
    }

    public static void AgainstDuplicate<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector, string message)
    {
        var duplicates = items.GroupBy(keySelector).Where(g => g.Count() > 1);
        if (duplicates.Any())
            throw new InvalidOperationException(message);
    }
}
