using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.IntegrationTests;

public class DatabaseTestBase : IDisposable
{
    protected readonly EcommerceDbContext Context;
    protected readonly ProductRepository ProductRepo;
    protected readonly CategoryRepository CategoryRepo;

    public DatabaseTestBase()
    {
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new EcommerceDbContext(options);
        ProductRepo = new ProductRepository(Context);
        CategoryRepo = new CategoryRepository(Context);

        SeedData();
    }

    private void SeedData()
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics",
            Slug = "electronics",
            Description = "Electronic devices"
        };

        Context.Categories.Add(category);
        Context.Products.AddRange(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Slug = "laptop",
                Price = 999.99m,
                StockQuantity = 50,
                Sku = "LAP-001",
                CategoryId = category.Id
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Slug = "smartphone",
                Price = 699.99m,
                StockQuantity = 100,
                Sku = "PHN-001",
                CategoryId = category.Id
            }
        );

        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}

public class ProductRepositoryTests : DatabaseTestBase
{
    [Fact]
    public async Task GetAll_ShouldReturnProducts()
    {
        var products = await ProductRepo.GetAllAsync();
        products.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct()
    {
        var allProducts = await ProductRepo.GetAllAsync();
        var firstProduct = allProducts.First();

        var product = await ProductRepo.GetByIdAsync(firstProduct.Id);
        product.Should().NotBeNull();
        product!.Name.Should().Be("Laptop");
    }

    [Fact]
    public async Task Add_ShouldCreateProduct()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Tablet",
            Slug = "tablet",
            Price = 499.99m,
            StockQuantity = 75,
            Sku = "TAB-001"
        };

        await ProductRepo.AddAsync(product);
        await Context.SaveChangesAsync();

        var allProducts = await ProductRepo.GetAllAsync();
        allProducts.Should().HaveCount(3);
    }

    [Fact]
    public async Task Update_ShouldModifyProduct()
    {
        var allProducts = await ProductRepo.GetAllAsync();
        var product = allProducts.First();
        product.Price = 899.99m;

        await ProductRepo.UpdateAsync(product);
        await Context.SaveChangesAsync();

        var updated = await ProductRepo.GetByIdAsync(product.Id);
        updated!.Price.Should().Be(899.99m);
    }

    [Fact]
    public async Task Delete_ShouldRemoveProduct()
    {
        var allProducts = await ProductRepo.GetAllAsync();
        var product = allProducts.First();

        await ProductRepo.DeleteAsync(product);
        await Context.SaveChangesAsync();

        var remaining = await ProductRepo.GetAllAsync();
        remaining.Should().HaveCount(1);
    }

    [Fact]
    public async Task Search_ShouldReturnMatchingProducts()
    {
        var results = await ProductRepo.SearchAsync("Laptop");
        results.Should().HaveCount(1);
        results.First().Name.Should().Be("Laptop");
    }

    [Fact]
    public async Task GetBySlug_ShouldReturnProduct()
    {
        var product = await ProductRepo.GetBySlugAsync("smartphone");
        product.Should().NotBeNull();
        product!.Name.Should().Be("Smartphone");
    }
}

public class CategoryRepositoryTests : DatabaseTestBase
{
    [Fact]
    public async Task GetAll_ShouldReturnCategories()
    {
        var categories = await CategoryRepo.GetAllAsync();
        categories.Should().HaveCount(1);
    }

    [Fact]
    public async Task Add_ShouldCreateCategory()
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Clothing",
            Slug = "clothing"
        };

        await CategoryRepo.AddAsync(category);
        await Context.SaveChangesAsync();

        var all = await CategoryRepo.GetAllAsync();
        all.Should().HaveCount(2);
    }
}

public class ApiHealthTests : DatabaseTestBase
{
    [Fact]
    public void Database_ShouldBeConnected()
    {
        Context.Database.CanConnect().Should().BeTrue();
    }

    [Fact]
    public async Task Products_ShouldBeQueryable()
    {
        var count = await Context.Products.CountAsync();
        count.Should().Be(2);
    }

    [Fact]
    public async Task Categories_ShouldBeQueryable()
    {
        var count = await Context.Categories.CountAsync();
        count.Should().Be(1);
    }
}
