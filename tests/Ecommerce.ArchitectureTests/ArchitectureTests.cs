using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace Ecommerce.ArchitectureTests;

public class DomainLayerTests
{
    private static readonly Assembly DomainAssembly = typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly;

    [Fact]
    public void Domain_ShouldNotDependOnApplication()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Application")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Domain layer should not depend on Application layer");
    }

    [Fact]
    public void Domain_ShouldNotDependOnInfrastructure()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Domain layer should not depend on Infrastructure layer");
    }

    [Fact]
    public void Domain_ShouldNotDependOnApi()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Domain layer should not depend on API layer");
    }

    [Fact]
    public void Domain_ShouldNotDependOnEntityFramework()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Domain layer should not depend on Entity Framework");
    }

    [Fact]
    public void Domain_ShouldNotDependOnAspNetCore()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.AspNetCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Domain layer should not depend on ASP.NET Core");
    }

    [Fact]
    public void Entities_ShouldInheritFromBaseEntity()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .HaveNameEndingWith("Entity")
            .Should()
            .Inherit(typeof(Ecommerce.Domain.Abstractions.BaseEntity))
            .GetResult();

        result.IsSuccessful.Should().BeTrue("All entities should inherit from BaseEntity");
    }

    [Fact]
    public void ValueObjects_ShouldBeRecords()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .HaveNameEndingWith("Value")
            .Or()
            .HaveNameEndingWith("Id")
            .Or()
            .HaveName("Money")
            .Or()
            .HaveName("Email")
            .Should()
            .MeetCustomRule(new IsRecordRule())
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Value objects should be records");
    }
}

public class ApplicationLayerTests
{
    private static readonly Assembly ApplicationAssembly = typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly;

    [Fact]
    public void Application_ShouldNotDependOnInfrastructure()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Application layer should not depend on Infrastructure layer");
    }

    [Fact]
    public void Application_ShouldNotDependOnApi()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Application layer should not depend on API layer");
    }

    [Fact]
    public void Application_ShouldNotDependOnEntityFramework()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Application layer should not depend on Entity Framework");
    }

    [Fact]
    public void DTOs_ShouldNotDependOnDomainEntities()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Dto")
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Domain.Entities")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("DTOs should not depend on Domain entities");
    }

    [Fact]
    public void Services_ShouldImplementInterfaces()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.IProductService))
            .Or()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.IOrderService))
            .Or()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.ICartService))
            .Or()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.ICategoryService))
            .Or()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.IReviewService))
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}

public class InfrastructureLayerTests
{
    private static readonly Assembly InfrastructureAssembly = typeof(Ecommerce.Infrastructure.Data.EcommerceDbContext).Assembly;

    [Fact]
    public void Infrastructure_ShouldNotDependOnApi()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Infrastructure layer should not depend on API layer");
    }

    [Fact]
    public void Infrastructure_ShouldNotDependOnWeb()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn("Ecommerce.Web")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Infrastructure layer should not depend on Web layer");
    }

    [Fact]
    public void Repositories_ShouldImplementInterfaces()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .ImplementInterface(typeof(Ecommerce.Application.Interfaces.IRepository<>))
            .GetResult();

        result.IsSuccessful.Should().BeTrue("All repositories should implement IRepository<T>");
    }

    [Fact]
    public void DbContext_ShouldBeInDataNamespace()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .HaveName("EcommerceDbContext")
            .Should()
            .ResideInNamespaceContaining("Data")
            .GetResult();

        result.IsSuccessful.Should().BeTrue("DbContext should be in Data namespace");
    }
}

public class NamingConventionTests
{
    [Fact]
    public void Entities_ShouldBeSingular()
    {
        var domainAssembly = typeof(Ecommerce.Domain.Entities.Catalog.Product).Assembly;
        var result = Types.InAssembly(domainAssembly)
            .That()
            .HaveNameEndingWith("Entity")
            .Should()
            .MeetCustomRule(new SingularNameRule())
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Entities should have singular names");
    }

    [Fact]
    public void DTOs_ShouldHaveDtoSuffix()
    {
        var appAssembly = typeof(Ecommerce.Application.DTOs.Product.ProductDto).Assembly;
        var result = Types.InAssembly(appAssembly)
            .That()
            .HaveNameEndingWith("Dto")
            .Or()
            .HaveNameEndingWith("Request")
            .Or()
            .HaveNameEndingWith("Response")
            .Should()
            .MeetCustomRule(new DtoNamingRule())
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}

public class IsRecordRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        return type.GetMethod("<Clone>$") != null ||
               type.GetProperties().Any(p => p.GetGetMethod()?.IsVirtual == true);
    }
}

public class SingularNameRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        return !type.Name.EndsWith("Entities") && !type.Name.EndsWith("Records");
    }
}

public class DtoNamingRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        return type.Name.EndsWith("Dto") || type.Name.EndsWith("Request") || type.Name.EndsWith("Response");
    }
}
