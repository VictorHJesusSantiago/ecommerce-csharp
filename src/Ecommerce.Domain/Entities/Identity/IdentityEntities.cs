using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Identity;

public class ApplicationUser : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string NormalizedEmail { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? AvatarUrl { get; private set; }
    public string? Company { get; private set; }
    public string? JobTitle { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public UserStatus Status { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool PhoneNumberConfirmed { get; private set; }
    public bool TwoFactorEnabled { get; private set; }
    public int AccessFailedCount { get; set; }
    public bool LockoutEnabled { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public DateTime? LastLoginAt { get; private set; }
    public string? LastLoginIp { get; private set; }
    public string? RegistrationIp { get; private set; }
    public string? ReferralCode { get; private set; }
    public Guid? ReferredById { get; private set; }
    public decimal WalletBalance { get; private set; }
    public int LoyaltyPoints { get; private set; }

    private readonly List<ApplicationUserRole> _roles = [];
    public IReadOnlyCollection<ApplicationUserRole> Roles => _roles.AsReadOnly();

    private readonly List<UserAddress> _addresses = [];
    public IReadOnlyCollection<UserAddress> Addresses => _addresses.AsReadOnly();

    private readonly List<UserPaymentMethod> _paymentMethods = [];
    public IReadOnlyCollection<UserPaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    private readonly List<UserRefreshToken> _refreshTokens = [];
    public IReadOnlyCollection<UserRefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private ApplicationUser() { }

    public static ApplicationUser Create(
        string email,
        string passwordHash,
        string? firstName = null,
        string? lastName = null,
        string? phoneNumber = null,
        string? referralCode = null,
        Guid? referredById = null,
        string? registrationIp = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        return new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = email.ToLowerInvariant().Trim(),
            NormalizedEmail = email.ToUpperInvariant().Trim(),
            PasswordHash = passwordHash,
            FirstName = firstName?.Trim(),
            LastName = lastName?.Trim(),
            PhoneNumber = phoneNumber?.Trim(),
            Status = UserStatus.PendingVerification,
            ReferralCode = referralCode,
            ReferredById = referredById,
            RegistrationIp = registrationIp,
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public string FullName => $"{FirstName} {LastName}".Trim();
    public string DisplayName => !string.IsNullOrEmpty(FirstName) ? FirstName : Email.Split('@')[0];

    public void UpdateProfile(string? firstName, string? lastName, string? phoneNumber,
        DateTime? dateOfBirth = null, Gender gender = Gender.Unknown, string? company = null,
        string? jobTitle = null)
    {
        FirstName = firstName?.Trim();
        LastName = lastName?.Trim();
        PhoneNumber = phoneNumber?.Trim();
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Company = company?.Trim();
        JobTitle = jobTitle?.Trim();
        UpdateTimestamp();
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        SecurityStamp = Guid.NewGuid().ToString();
        UpdateTimestamp();
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
        if (Status == UserStatus.PendingVerification)
            Status = UserStatus.Active;
        UpdateTimestamp();
    }

    public void ConfirmPhoneNumber()
    {
        PhoneNumberConfirmed = true;
        UpdateTimestamp();
    }

    public void RecordLogin(string? ipAddress)
    {
        LastLoginAt = DateTime.UtcNow;
        LastLoginIp = ipAddress;
        AccessFailedCount = 0;
        UpdateTimestamp();
    }

    public void Lock(DateTime until)
    {
        LockoutEnd = until;
        LockoutEnabled = true;
        Status = UserStatus.Locked;
        UpdateTimestamp();
    }

    public void Unlock()
    {
        LockoutEnd = null;
        LockoutEnabled = false;
        Status = UserStatus.Active;
        AccessFailedCount = 0;
        UpdateTimestamp();
    }

    public void Suspend(string? reason = null)
    {
        Status = UserStatus.Suspended;
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        Status = UserStatus.Inactive;
        UpdateTimestamp();
    }

    public void Activate()
    {
        Status = UserStatus.Active;
        UpdateTimestamp();
    }

    public void AddRole(ApplicationUserRole role)
    {
        if (_roles.Any(r => r.RoleId == role.RoleId))
            throw new InvalidDomainOperationException("User already has this role.");
        _roles.Add(role);
        UpdateTimestamp();
    }

    public void RemoveRole(Guid roleId)
    {
        var role = _roles.FirstOrDefault(r => r.RoleId == roleId);
        if (role is not null)
        {
            _roles.Remove(role);
            UpdateTimestamp();
        }
    }

    public bool HasRole(string roleName)
    {
        return _roles.Any(r => r.Role.Name == roleName);
    }

    public void AddAddress(UserAddress address)
    {
        if (address is null) throw new ArgumentNullException(nameof(address));
        _addresses.Add(address);
        UpdateTimestamp();
    }

    public void RemoveAddress(Guid addressId)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == addressId);
        if (address is not null)
        {
            _addresses.Remove(address);
            UpdateTimestamp();
        }
    }

    public UserAddress? GetDefaultShippingAddress()
    {
        return _addresses.FirstOrDefault(a => a.IsDefault && a.AddressType == AddressType.Shipping && !a.IsDeleted);
    }

    public UserAddress? GetDefaultBillingAddress()
    {
        return _addresses.FirstOrDefault(a => a.IsDefault && a.AddressType == AddressType.Billing && !a.IsDeleted);
    }

    public void AddPaymentMethod(UserPaymentMethod method)
    {
        if (method is null) throw new ArgumentNullException(nameof(method));
        _paymentMethods.Add(method);
        UpdateTimestamp();
    }

    public void RemovePaymentMethod(Guid methodId)
    {
        var method = _paymentMethods.FirstOrDefault(m => m.Id == methodId);
        if (method is not null)
        {
            _paymentMethods.Remove(method);
            UpdateTimestamp();
        }
    }

    public void AddWalletBalance(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive.", nameof(amount));
        WalletBalance += amount;
        UpdateTimestamp();
    }

    public bool DeductWalletBalance(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive.", nameof(amount));
        if (WalletBalance < amount) return false;
        WalletBalance -= amount;
        UpdateTimestamp();
        return true;
    }

    public void AddLoyaltyPoints(int points)
    {
        if (points < 0) throw new ArgumentException("Points cannot be negative.", nameof(points));
        LoyaltyPoints += points;
        UpdateTimestamp();
    }

    public bool RedeemLoyaltyPoints(int points)
    {
        if (points < 0) throw new ArgumentException("Points cannot be negative.", nameof(points));
        if (LoyaltyPoints < points) return false;
        LoyaltyPoints -= points;
        UpdateTimestamp();
        return true;
    }
}

public class ApplicationRole : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsSystemRole { get; private set; }

    private readonly List<ApplicationUserRole> _userRoles = [];
    public IReadOnlyCollection<ApplicationUserRole> UserRoles => _userRoles.AsReadOnly();

    private readonly List<ApplicationRolePermission> _permissions = [];
    public IReadOnlyCollection<ApplicationRolePermission> Permissions => _permissions.AsReadOnly();

    private ApplicationRole() { }

    public static ApplicationRole Create(string name, string? description = null, bool isSystemRole = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name is required.", nameof(name));

        return new ApplicationRole
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            NormalizedName = name.ToUpperInvariant().Trim(),
            Description = description?.Trim(),
            IsSystemRole = isSystemRole,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddPermission(ApplicationRolePermission permission)
    {
        if (_permissions.Any(p => p.PermissionId == permission.PermissionId))
            throw new InvalidDomainOperationException("Role already has this permission.");
        _permissions.Add(permission);
        UpdateTimestamp();
    }

    public void RemovePermission(Guid permissionId)
    {
        var permission = _permissions.FirstOrDefault(p => p.PermissionId == permissionId);
        if (permission is not null)
        {
            _permissions.Remove(permission);
            UpdateTimestamp();
        }
    }

    public bool HasPermission(Permission permission)
    {
        return _permissions.Any(p => p.Permission.Name == permission.ToString());
    }
}

public class ApplicationUserRole : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public DateTime? AssignedAt { get; private set; }
    public string? AssignedBy { get; private set; }
    public ApplicationUser User { get; private set; } = null!;
    public ApplicationRole Role { get; private set; } = null!;

    private ApplicationUserRole() { }

    public static ApplicationUserRole Create(Guid userId, Guid roleId, string? assignedBy = null)
    {
        return new ApplicationUserRole
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RoleId = roleId,
            AssignedAt = DateTime.UtcNow,
            AssignedBy = assignedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class ApplicationRolePermission : BaseEntity
{
    public Guid RoleId { get; private set; }
    public Guid PermissionId { get; private set; }
    public ApplicationRole Role { get; private set; } = null!;
    public ApplicationPermission Permission { get; private set; } = null!;

    private ApplicationRolePermission() { }

    public static ApplicationRolePermission Create(Guid roleId, Guid permissionId)
    {
        return new ApplicationRolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = roleId,
            PermissionId = permissionId,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class ApplicationPermission : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Group { get; private set; } = string.Empty;

    private ApplicationPermission() { }

    public static ApplicationPermission Create(string name, string group, string? description = null)
    {
        return new ApplicationPermission
        {
            Id = Guid.NewGuid(),
            Name = name,
            NormalizedName = name.ToUpperInvariant(),
            Description = description,
            Group = group,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class UserAddress : BaseEntity
{
    public Guid UserId { get; private set; }
    public AddressType AddressType { get; private set; }
    public string Label { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string? Street2 { get; private set; }
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string? Landmark { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool IsDefault { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    private UserAddress() { }

    public static UserAddress Create(
        Guid userId,
        AddressType addressType,
        string label,
        string street,
        string city,
        string state,
        string postalCode,
        string country,
        string? street2 = null,
        string? landmark = null,
        string? phoneNumber = null,
        bool isDefault = false)
    {
        return new UserAddress
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AddressType = addressType,
            Label = label.Trim(),
            Street = street.Trim(),
            Street2 = street2?.Trim(),
            City = city.Trim(),
            State = state.Trim(),
            PostalCode = postalCode.Trim(),
            Country = country.Trim(),
            Landmark = landmark?.Trim(),
            PhoneNumber = phoneNumber?.Trim(),
            IsDefault = isDefault,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        AddressType addressType,
        string label,
        string street,
        string city,
        string state,
        string postalCode,
        string country,
        string? street2 = null,
        string? landmark = null,
        string? phoneNumber = null,
        bool isDefault = false)
    {
        AddressType = addressType;
        Label = label.Trim();
        Street = street.Trim();
        Street2 = street2?.Trim();
        City = city.Trim();
        State = state.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();
        Landmark = landmark?.Trim();
        PhoneNumber = phoneNumber?.Trim();
        IsDefault = isDefault;
        UpdateTimestamp();
    }

    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
        UpdateTimestamp();
    }
}

public class UserPaymentMethod : BaseEntity
{
    public Guid UserId { get; private set; }
    public PaymentMethod Type { get; private set; }
    public string Last4Digits { get; private set; } = string.Empty;
    public string? CardBrand { get; private set; }
    public int? ExpiryMonth { get; private set; }
    public int? ExpiryYear { get; private set; }
    public string? HolderName { get; private set; }
    public string? Token { get; private set; }
    public string? GatewayCustomerId { get; private set; }
    public string? GatewayPaymentMethodId { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    private UserPaymentMethod() { }

    public static UserPaymentMethod Create(
        Guid userId,
        PaymentMethod type,
        string last4Digits,
        string? cardBrand = null,
        int? expiryMonth = null,
        int? expiryYear = null,
        string? holderName = null,
        string? token = null,
        string? gatewayCustomerId = null,
        string? gatewayPaymentMethodId = null,
        bool isDefault = false)
    {
        return new UserPaymentMethod
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = type,
            Last4Digits = last4Digits,
            CardBrand = cardBrand,
            ExpiryMonth = expiryMonth,
            ExpiryYear = expiryYear,
            HolderName = holderName?.Trim(),
            Token = token,
            GatewayCustomerId = gatewayCustomerId,
            GatewayPaymentMethodId = gatewayPaymentMethodId,
            IsDefault = isDefault,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
        UpdateTimestamp();
    }

    public bool IsExpired()
    {
        if (!ExpiryMonth.HasValue || !ExpiryYear.HasValue) return false;
        var now = DateTime.UtcNow;
        return now.Year > ExpiryYear.Value || (now.Year == ExpiryYear.Value && now.Month > ExpiryMonth.Value);
    }
}

public class UserRefreshToken : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public string? JwtId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? RevokedByIp { get; private set; }
    public string? CreatedByIp { get; private set; }
    public string? ReplacedByToken { get; private set; }
    public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
    public ApplicationUser User { get; private set; } = null!;

    private UserRefreshToken() { }

    public static UserRefreshToken Create(
        Guid userId,
        string token,
        int expiresInDays = 7,
        string? jwtId = null,
        string? createdByIp = null)
    {
        return new UserRefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            JwtId = jwtId,
            ExpiresAt = DateTime.UtcNow.AddDays(expiresInDays),
            CreatedByIp = createdByIp,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Revoke(string? revokedByIp = null, string? replacedByToken = null)
    {
        RevokedAt = DateTime.UtcNow;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;
        UpdateTimestamp();
    }
}

public class UserActivity : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string? Details { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public string? Resource { get; private set; }
    public bool IsSuccess { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    private UserActivity() { }

    public static UserActivity Create(
        Guid userId,
        string action,
        string? details = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? resource = null,
        bool isSuccess = true)
    {
        return new UserActivity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Action = action,
            Details = details,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            Resource = resource,
            IsSuccess = isSuccess,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class UserWishlist : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public WishlistStatus Status { get; private set; }
    public string? ShareToken { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    private readonly List<UserWishlistItem> _items = [];
    public IReadOnlyCollection<UserWishlistItem> Items => _items.AsReadOnly();

    private UserWishlist() { }

    public static UserWishlist Create(Guid userId, string name = "My Wishlist")
    {
        return new UserWishlist
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name.Trim(),
            Status = WishlistStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddItem(Guid productId)
    {
        if (_items.Any(i => i.ProductId == productId))
            throw new InvalidDomainOperationException("Product already in wishlist.");
        _items.Add(UserWishlistItem.Create(Id, productId));
        UpdateTimestamp();
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            UpdateTimestamp();
        }
    }

    public bool ContainsProduct(Guid productId) => _items.Any(i => i.ProductId == productId);
    public int ItemCount => _items.Count;

    public void Share()
    {
        Status = WishlistStatus.Shared;
        ShareToken = Guid.NewGuid().ToString("N");
        UpdateTimestamp();
    }

    public void Unshare()
    {
        Status = WishlistStatus.Active;
        ShareToken = null;
        UpdateTimestamp();
    }
}

public class UserWishlistItem : BaseEntity
{
    public Guid WishlistId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Priority { get; private set; }
    public string? Notes { get; private set; }
    public UserWishlist Wishlist { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private UserWishlistItem() { }

    public static UserWishlistItem Create(Guid wishlistId, Guid productId, int priority = 0, string? notes = null)
    {
        return new UserWishlistItem
        {
            Id = Guid.NewGuid(),
            WishlistId = wishlistId,
            ProductId = productId,
            Priority = priority,
            Notes = notes,
            CreatedAt = DateTime.UtcNow
        };
    }
}
