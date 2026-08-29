using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace Ecommerce.ArchitectureTests;

public class DependencyTests
{
    [Fact]
    public void Domain_ShouldNotDependOnApplicationLayer()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Application")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_ShouldNotDependOnInfrastructureLayer()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Infrastructure")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_ShouldNotDependOnEntityFramework()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_ShouldNotDependOnInfrastructureLayer()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Infrastructure")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_ShouldNotDependOnEntityFramework()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_ShouldNotDependOnApiLayer()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Infrastructure.Data.EcommerceDbContext).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Api")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }
}

public class NamingTests
{
    [Fact]
    public void Entities_ShouldBeInCorrectNamespace()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly)
            .That()
            .HaveNameEndingWith("Entity")
            .Should()
            .ResideInNamespaceContaining("Entities")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Services_ShouldHaveServiceSuffix()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Application.Services.ProductService).Assembly)
            .That()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.IProductService))
            .Should()
            .HaveNameEndingWith("Service")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Repositories_ShouldImplementIRepository()
    {
        var result = Types.InAssembly(typeof(Ecommerce.Infrastructure.Data.EcommerceDbContext).Assembly)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.IRepository<>))
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }
}

public class ComplexityTests
{
    [Fact]
    public void DomainEntities_ShouldNotBeTooLarge()
    {
        var types = Types.InAssembly(typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly)
            .That()
            .HaveNameEndingWith("Entity")
            .Or()
            .HaveName("Product")
            .Or()
            .HaveName("Order")
            .Or()
            .HaveName("Category")
            .GetTypes();

        foreach (var type in types)
        {
            var methodCount = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Length;
            methodCount.Should().BeLessThan(30, $"Entity {type.Name} has too many public methods ({methodCount})");
        }
    }
}
