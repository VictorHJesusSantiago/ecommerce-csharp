using Ecommerce.Domain.Entities.User;

namespace Ecommerce.Web.Models.Account;

public class AddressViewModel
{
    public Guid Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string? Street2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool IsDefault { get; set; }

    public static AddressViewModel FromEntity(Address entity) => new()
    {
        Id = entity.Id,
        Label = entity.Label,
        FullName = entity.FullName,
        Street = entity.Street,
        Street2 = entity.Street2,
        City = entity.City,
        State = entity.State,
        PostalCode = entity.PostalCode,
        Country = entity.Country,
        Phone = entity.Phone,
        IsDefault = entity.IsDefault
    };

    public Address ToEntity() => new()
    {
        Id = Id,
        Label = Label,
        FullName = FullName,
        Street = Street,
        Street2 = Street2,
        City = City,
        State = State,
        PostalCode = PostalCode,
        Country = Country,
        Phone = Phone,
        IsDefault = IsDefault
    };
}
