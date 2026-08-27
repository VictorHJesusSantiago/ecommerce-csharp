namespace Ecommerce.Application.DTOs.CMS;

public class CmsPageDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Content { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgImage { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? TemplateName { get; set; }
    public string? CustomCss { get; set; }
    public string? CustomJs { get; set; }
    public bool IsPublished { get; set; }
    public bool IsSystemPage { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public List<CmsPageRevisionDto> Revisions { get; set; } = [];
}

public class CreateCmsPageRequest
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Content { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgImage { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? TemplateName { get; set; }
    public string? CustomCss { get; set; }
    public string? CustomJs { get; set; }
    public bool IsPublished { get; set; }
}

public class UpdateCmsPageRequest
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgImage { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? TemplateName { get; set; }
    public string? CustomCss { get; set; }
    public string? CustomJs { get; set; }
    public bool? IsPublished { get; set; }
}

public class CmsPageRevisionDto
{
    public Guid Id { get; set; }
    public Guid PageId { get; set; }
    public int Version { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string? ChangeNote { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class NavigationMenuDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Position { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public List<NavigationMenuItemDto> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class NavigationMenuItemDto
{
    public Guid Id { get; set; }
    public Guid MenuId { get; set; }
    public string Label { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Target { get; set; }
    public string? IconClass { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? PageId { get; set; }
    public string? PageSlug { get; set; }
    public Guid? ParentItemId { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public bool OpenInNewTab { get; set; }
    public string? CssClass { get; set; }
    public string? Tooltip { get; set; }
    public List<NavigationMenuItemDto> Children { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class CreateNavigationMenuRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Position { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public List<CreateNavigationMenuItemRequest> Items { get; set; } = [];
}

public class CreateNavigationMenuItemRequest
{
    public string Label { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Target { get; set; }
    public string? IconClass { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? PageId { get; set; }
    public Guid? ParentItemId { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public bool OpenInNewTab { get; set; }
    public string? CssClass { get; set; }
    public string? Tooltip { get; set; }
}

public class SiteSettingDto
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
    public string ValueType { get; set; } = "String";
    public string? Group { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public bool IsRequired { get; set; }
    public string? DefaultValue { get; set; }
    public string? ValidationRule { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class UpdateSiteSettingRequest
{
    public string Value { get; set; } = string.Empty;
}

public class BulkUpdateSiteSettingsRequest
{
    public Dictionary<string, string> Settings { get; set; } = [];
}

public class MediaFileDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileSizeFormatted { get; set; } = string.Empty;
    public string? AltText { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string Folder { get; set; } = string.Empty;
    public string? Extension { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsImage { get; set; }
    public bool IsVideo { get; set; }
    public bool IsDocument { get; set; }
    public int DownloadCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class UploadMediaRequest
{
    public string? Folder { get; set; }
    public string? AltText { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool OverwriteExisting { get; set; }
}

public class MediaFolderDto
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int FileCount { get; set; }
    public long TotalSize { get; set; }
    public string TotalSizeFormatted { get; set; } = string.Empty;
    public List<MediaFolderDto> SubFolders { get; set; } = [];
}

public class SeoMetadataDto
{
    public Guid? PageId { get; set; }
    public string? PageType { get; set; }
    public string MetaTitle { get; set; } = string.Empty;
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgImage { get; set; }
    public string? OgType { get; set; }
    public string? TwitterCard { get; set; }
    public string? TwitterTitle { get; set; }
    public string? TwitterDescription { get; set; }
    public string? TwitterImage { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? Robots { get; set; }
    public string? SchemaMarkup { get; set; }
    public DateTime? LastModified { get; set; }
}

public class CmsSearchRequest
{
    public string? Query { get; set; }
    public string? ContentType { get; set; }
    public bool? IsPublished { get; set; }
    public DateTime? PublishedAfter { get; set; }
    public DateTime? PublishedBefore { get; set; }
    public string? Author { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class CmsPagePreviewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Content { get; set; }
    public string? TemplateName { get; set; }
    public string? CustomCss { get; set; }
    public SeoMetadataDto? SeoMetadata { get; set; }
    public List<CmsPageContentSectionDto> Sections { get; set; } = [];
}

public class CmsPageContentSectionDto
{
    public string Type { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public string? LinkUrl { get; set; }
    public string? LinkText { get; set; }
    public string? BackgroundColor { get; set; }
    public string? TextColor { get; set; }
    public Dictionary<string, object> Settings { get; set; } = [];
}
