using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Catalog;

public class Category : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? BannerUrl { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFeatured { get; private set; }
    public Guid? ParentCategoryId { get; private set; }
    public string? MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeywords { get; private set; }

    private readonly List<Category> _subcategories = [];
    public IReadOnlyCollection<Category> Subcategories => _subcategories.AsReadOnly();

    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Category() { }

    public static Category Create(
        string name,
        string slug,
        string? description = null,
        string? imageUrl = null,
        Guid? parentCategoryId = null,
        int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.", nameof(name));
        if (name.Length > 200)
            throw new ArgumentException("Category name cannot exceed 200 characters.", nameof(name));

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Slug = slug ?? Slug.Create(name).Value,
            Description = description?.Trim(),
            ImageUrl = imageUrl,
            ParentCategoryId = parentCategoryId,
            DisplayOrder = displayOrder,
            IsActive = true,
            IsFeatured = false,
            CreatedAt = DateTime.UtcNow
        };

        category.AddDomainEvent(new Events.Catalog.CategoryCreatedEvent(category.Id, category.Name));
        return category;
    }

    public void Update(
        string name,
        string? description = null,
        string? imageUrl = null,
        int displayOrder = 0,
        bool isActive = true,
        bool isFeatured = false,
        string? metaTitle = null,
        string? metaDescription = null,
        string? metaKeywords = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.", nameof(name));

        Name = name.Trim();
        Description = description?.Trim();
        ImageUrl = imageUrl;
        DisplayOrder = displayOrder;
        IsActive = isActive;
        IsFeatured = isFeatured;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeywords = metaKeywords;
        UpdateTimestamp();

        AddDomainEvent(new Events.Catalog.CategoryUpdatedEvent(Id, Name));
    }

    public void UpdateSlug(string newSlug)
    {
        Slug = newSlug ?? throw new ArgumentNullException(nameof(newSlug));
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimestamp();
    }

    public void AddSubcategory(Category subcategory)
    {
        if (subcategory is null)
            throw new ArgumentNullException(nameof(subcategory));
        if (_subcategories.Any(s => s.Id == subcategory.Id))
            throw new InvalidDomainOperationException("Subcategory already exists.");
        if (subcategory.Id == Id)
            throw new InvalidDomainOperationException("A category cannot be its own subcategory.");

        _subcategories.Add(subcategory);
        UpdateTimestamp();
    }

    public void RemoveSubcategory(Guid subcategoryId)
    {
        var subcategory = _subcategories.FirstOrDefault(s => s.Id == subcategoryId);
        if (subcategory is not null)
        {
            _subcategories.Remove(subcategory);
            UpdateTimestamp();
        }
    }

    public void AddProduct(Product product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));
        _products.Add(product);
        UpdateTimestamp();
    }

    public void RemoveProduct(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product is not null)
        {
            _products.Remove(product);
            UpdateTimestamp();
        }
    }

    public int GetProductCount() => _products.Count(p => !p.IsDeleted);
}
