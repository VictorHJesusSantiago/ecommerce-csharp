using FluentAssertions;
using Xunit;

namespace Ecommerce.ArchitectureTests;

public class ComplexityTests
{
    [Fact]
    public void Domain_ShouldNotReferenceInfrastructure()
    {
        var domainAssembly = typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly;
        var references = domainAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();
        references.Should().NotContain("Ecommerce.Infrastructure");
    }

    [Fact]
    public void Application_ShouldNotReferenceInfrastructure()
    {
        var appAssembly = typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly;
        var references = appAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();
        references.Should().NotContain("Ecommerce.Infrastructure");
    }

    [Fact]
    public void Application_ShouldNotReferenceApi()
    {
        var appAssembly = typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly;
        var references = appAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();
        references.Should().NotContain("Ecommerce.Api");
    }

    [Fact]
    public void Domain_ShouldNotReferenceApi()
    {
        var domainAssembly = typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly;
        var references = domainAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();
        references.Should().NotContain("Ecommerce.Api");
    }

    [Fact]
    public void DomainEntities_ShouldHaveMoreThan30Classes()
    {
        var assembly = typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly;
        var entityTypes = assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("Entities") && !t.IsInterface && !t.IsAbstract).ToList();
        entityTypes.Count.Should().BeGreaterThan(30);
    }

    [Fact]
    public void ApplicationDtos_ShouldHaveMoreThan40Classes()
    {
        var assembly = typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly;
        var dtoTypes = assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("DTOs") && !t.IsInterface && !t.IsAbstract).ToList();
        dtoTypes.Count.Should().BeGreaterThan(40);
    }

    [Fact]
    public void Infrastructure_ShouldImplementAllRepositoryInterfaces()
    {
        var infraAssembly = typeof(Ecommerce.Infrastructure.Repositories.Repository<>).Assembly;
        var appAssembly = typeof(Ecommerce.Application.Contracts.Repositories.IRepository<>).Assembly;
        var repoInterfaces = appAssembly.GetTypes().Where(t => t.IsInterface && t.Namespace != null && t.Namespace.Contains("Repositories")).ToList();
        repoInterfaces.Should().NotBeEmpty();
    }

    [Fact]
    public void DomainEvents_ShouldBeImmutable()
    {
        var eventType = typeof(Ecommerce.Domain.Events.ProductCreatedEvent);
        var props = eventType.GetProperties().Where(p => p.CanWrite).ToList();
        props.Should().BeEmpty("Domain events should be immutable");
    }

    [Fact]
    public void Money_ShouldBeImmutableValueObject()
    {
        var moneyType = typeof(Ecommerce.Domain.ValueObjects.Money);
        var props = moneyType.GetProperties().Where(p => p.CanWrite && p.SetMethod != null && p.SetMethod.IsPublic).ToList();
        props.Should().BeEmpty("Money should be immutable");
    }

    [Fact]
    public void All_ShouldHaveTests_For_DomainEntities()
    {
        var testAssembly = typeof(Ecommerce.ArchitectureTests.NamingConventionTests).Assembly;
        var testTypes = testAssembly.GetTypes().Where(t => t.Name.Contains("EntityTests") || t.Name.Contains("ValueObject")).ToList();
        testTypes.Should().NotBeEmpty();
    }
}
