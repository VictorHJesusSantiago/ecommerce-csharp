using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Cms;

public class CmsPage : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public bool IsPublished { get; set; }
    public string? AuthorId { get; set; }
    public string? TemplateName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual ICollection<CmsPageRevision> Revisions { get; set; } = new List<CmsPageRevision>();
}

public class CmsPageRevision : BaseEntity
{
    public Guid PageId { get; set; }
    public int Version { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? AuthorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual CmsPage Page { get; set; } = null!;
}

public class NavigationMenu : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<NavigationMenuItem> Items { get; set; } = new List<NavigationMenuItem>();
}

public class NavigationMenuItem : BaseEntity
{
    public Guid MenuId { get; set; }
    public string Label { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public Guid? PageId { get; set; }
    public Guid? CategoryId { get; set; }
    public int SortOrder { get; set; }
    public bool IsExternal { get; set; }
    public bool OpensInNewTab { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? ParentId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual NavigationMenu Menu { get; set; } = null!;
    public virtual NavigationMenuItem? Parent { get; set; }
    public virtual ICollection<NavigationMenuItem> Children { get; set; } = new List<NavigationMenuItem>();
    public virtual CmsPage? Page { get; set; }
    public virtual Catalog.Category? Category { get; set; }
}

public class SiteSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Group { get; set; }
    public string? Description { get; set; }
    public string? DefaultValue { get; set; }
    public string? DataType { get; set; } // "string", "int", "bool", "json"
    public bool IsEncrypted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public class MediaFile : BaseEntity
{
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string? AltText { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Folder { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? UploadedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string FileSizeFormatted => FileSize < 1024 ? $"{FileSize} B" :
        FileSize < 1048576 ? $"{FileSize / 1024.0:F1} KB" :
        $"{FileSize / 1048576.0:F1} MB";
}
