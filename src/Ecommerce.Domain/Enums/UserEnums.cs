namespace Ecommerce.Domain.Enums;

public enum UserRole
{
    Customer = 0,
    Admin = 1,
    SuperAdmin = 2,
    Vendor = 3,
    Manager = 4,
    Support = 5,
    ReadOnly = 6
}

public enum UserStatus
{
    Active = 0,
    Inactive = 1,
    Suspended = 2,
    PendingVerification = 3,
    Locked = 4,
    Banned = 5
}

public enum Gender
{
    Unknown = 0,
    Male = 1,
    Female = 2,
    NonBinary = 3,
    PreferNotToSay = 4
}

public enum AddressType
{
    Home = 0,
    Work = 1,
    Billing = 2,
    Shipping = 3,
    Other = 4
}

public enum PhoneType
{
    Mobile = 0,
    Home = 1,
    Work = 2,
    Fax = 3,
    Other = 4
}

public enum Permission
{
    ViewProducts = 0,
    ManageProducts = 1,
    ViewCategories = 2,
    ManageCategories = 3,
    ViewOrders = 4,
    ManageOrders = 5,
    ViewUsers = 6,
    ManageUsers = 7,
    ViewReports = 8,
    ManageMarketing = 9,
    ManageInventory = 10,
    ManageSettings = 11,
    ProcessPayments = 12,
    ManageCms = 13,
    ViewAnalytics = 14,
    ManageShipping = 15
}
