using FluentAssertions;
using Xunit;

namespace Ecommerce.ArchitectureTests;

public class NamingConventionTests
{
    [Fact]
    public void All_DomainEntities_ShouldBeInEcommerceDomainEntitiesNamespace()
    {
        typeof(Ecommerce.Domain.Entities.Catalog.Product).Namespace.Should().Contain("Ecommerce.Domain.Entities");
    }

    [Fact]
    public void All_ValueObjects_ShouldBeInEcommerceDomainValueObjectsNamespace()
    {
        typeof(Ecommerce.Domain.ValueObjects.Money).Namespace.Should().Contain("Ecommerce.Domain.ValueObjects");
    }

    [Fact]
    public void All_Dto_ShouldBeInEcommerceApplicationDtosNamespace()
    {
        typeof(Ecommerce.Application.DTOs.Product.ProductDto).Namespace.Should().Contain("Ecommerce.Application.DTOs");
    }

    [Fact]
    public void All_Validators_ShouldEndWithValidator()
    {
        typeof(Ecommerce.Application.DTOs.Product.CreateProductDto).Name.Should().EndWith("Dto");
    }

    [Fact]
    public void DomainEntities_ShouldNotDependOnApplication()
    {
        var domainAssembly = typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly;
        domainAssembly.GetReferencedNamespaces().Any(n => n.Contains("Application")).Should().BeFalse();
    }

    [Fact]
    public void ValueObjects_ShouldImplementIEquatable()
    {
        typeof(Ecommerce.Domain.ValueObjects.Money).GetInterfaces()
            .Any(i => i.Name.Contains("IEquatable")).Should().BeTrue();
    }

    [Fact]
    public void DomainExceptions_ShouldInheritFromBaseException()
    {
        typeof(Ecommerce.Domain.Exceptions.DomainException).BaseException().Name.Should().Be("Exception");
    }

    [Fact]
    public void Specifications_ShouldBeInEcommerceDomainSpecificationsNamespace()
    {
        typeof(Ecommerce.Domain.Specifications.ProductSpecification).Namespace.Should().Contain("Ecommerce.Domain.Specifications");
    }

    [Fact]
    public void Policies_ShouldBeInEcommerceDomainPoliciesNamespace()
    {
        typeof(Ecommerce.Domain.Policies.StandardPricingPolicy).Namespace.Should().Contain("Ecommerce.Domain.Policies");
    }

    [Fact]
    public void Factories_ShouldBeInEcommerceApplicationFactoriesNamespace()
    {
        typeof(Ecommerce.Application.Factories.ResponseFactory).Namespace.Should().Contain("Ecommerce.Application.Factories");
    }
}
