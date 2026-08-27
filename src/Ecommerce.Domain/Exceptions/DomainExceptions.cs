namespace Ecommerce.Domain.Exceptions;

public class DomainException : Exception
{
    public string Code { get; }
    public DomainException(string message, string code = "DomainError") : base(message)
    {
        Code = code;
    }
    public DomainException(string message, Exception innerException, string code = "DomainError") : base(message, innerException)
    {
        Code = code;
    }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.", "NotFound") { }
}

public class BadRequestException : DomainException
{
    public BadRequestException(string message) : base(message, "BadRequest") { }
}

public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message, "Conflict") { }
}

public class UnauthorizedDomainException : DomainException
{
    public UnauthorizedDomainException(string message = "Unauthorized") : base(message, "Unauthorized") { }
}

public class ForbiddenDomainException : DomainException
{
    public ForbiddenDomainException(string message = "Forbidden") : base(message, "Forbidden") { }
}

public class ValidationDomainException : DomainException
{
    public IEnumerable<string> Errors { get; }
    public ValidationDomainException(IEnumerable<string> errors) : base("Validation failed.", "Validation")
    {
        Errors = errors;
    }
}

public class ConcurrencyException : DomainException
{
    public ConcurrencyException() : base("A concurrency error occurred. Please try again.", "Concurrency") { }
}

public class EntityTooLargeException : DomainException
{
    public EntityTooLargeException(string entityName, int maxSize)
        : base($"Entity \"{entityName}\" exceeds maximum size of {maxSize}.", "EntityTooLarge") { }
}

public class RateLimitExceededException : DomainException
{
    public RateLimitExceededException() : base("Rate limit exceeded. Please try again later.", "RateLimit") { }
}

public class ExternalServiceException : DomainException
{
    public string ServiceName { get; }
    public ExternalServiceException(string serviceName, string message)
        : base($"External service error ({serviceName}): {message}", "ExternalService")
    {
        ServiceName = serviceName;
    }
}
