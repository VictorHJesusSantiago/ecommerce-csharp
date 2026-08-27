namespace Ecommerce.Domain.Events.User;

public class UserRegisteredEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FullName { get; }

    public UserRegisteredEvent(Guid userId, string email, string fullName)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
    }
}

public class UserUpdatedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string FullName { get; }

    public UserUpdatedEvent(Guid userId, string fullName)
    {
        UserId = userId;
        FullName = fullName;
    }
}

public class UserDeactivatedEvent : DomainEvent
{
    public Guid UserId { get; }

    public UserDeactivatedEvent(Guid userId)
    {
        UserId = userId;
    }
}

public class UserPasswordChangedEvent : DomainEvent
{
    public Guid UserId { get; }

    public UserPasswordChangedEvent(Guid userId)
    {
        UserId = userId;
    }
}

public class UserRoleChangedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string OldRole { get; }
    public string NewRole { get; }

    public UserRoleChangedEvent(Guid userId, string oldRole, string newRole)
    {
        UserId = userId;
        OldRole = oldRole;
        NewRole = newRole;
    }
}
