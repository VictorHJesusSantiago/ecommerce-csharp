using MediatR;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Application.Handlers;

public class GetCmsPageBySlugHandler : IRequestHandler<GetCmsPageBySlugQuery, ApiResponse<CmsPageDto>>
{
    public async Task<ApiResponse<CmsPageDto>> Handle(GetCmsPageBySlugQuery request, CancellationToken ct)
    {
        return ApiResponse<CmsPageDto>.SuccessResponse(new CmsPageDto
        {
            Id = Guid.NewGuid(),
            Title = "About Us",
            Slug = request.Slug,
            Content = "<h1>About Us</h1><p>We are a leading e-commerce platform.</p>",
            IsPublished = true,
            MetaTitle = "About Us - Our Story",
            MetaDescription = "Learn about our company and mission.",
            CreatedAt = DateTime.UtcNow
        });
    }
}

public class CreateCmsPageHandler : IRequestHandler<CreateCmsPageCommand, ApiResponse<CmsPageDto>>
{
    public async Task<ApiResponse<CmsPageDto>> Handle(CreateCmsPageCommand request, CancellationToken ct)
    {
        return ApiResponse<CmsPageDto>.SuccessResponse(new CmsPageDto
        {
            Id = Guid.NewGuid(),
            Title = request.Request.Title,
            Slug = request.Request.Slug,
            Content = request.Request.Content,
            IsPublished = request.Request.IsPublished,
            MetaTitle = request.Request.MetaTitle,
            MetaDescription = request.Request.MetaDescription,
            CreatedAt = DateTime.UtcNow
        }, "Page created successfully");
    }
}

public class UpdateCmsPageHandler : IRequestHandler<UpdateCmsPageCommand, ApiResponse<CmsPageDto>>
{
    public async Task<ApiResponse<CmsPageDto>> Handle(UpdateCmsPageCommand request, CancellationToken ct)
    {
        return ApiResponse<CmsPageDto>.SuccessResponse(new CmsPageDto
        {
            Id = request.Id,
            Title = request.Request.Title ?? "Updated Page",
            Slug = "updated-page",
            Content = request.Request.Content,
            IsPublished = request.Request.IsPublished ?? true,
            UpdatedAt = DateTime.UtcNow
        }, "Page updated successfully");
    }
}

public class DeleteCmsPageHandler : IRequestHandler<DeleteCmsPageCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteCmsPageCommand request, CancellationToken ct)
    {
        return ApiResponse.SuccessResponse("Page deleted successfully");
    }
}

public class GetNavigationMenuHandler : IRequestHandler<GetNavigationMenuQuery, ApiResponse<NavigationMenuDto>>
{
    public async Task<ApiResponse<NavigationMenuDto>> Handle(GetNavigationMenuQuery request, CancellationToken ct)
    {
        return ApiResponse<NavigationMenuDto>.SuccessResponse(new NavigationMenuDto
        {
            Id = Guid.NewGuid(),
            Name = "Main Menu",
            Position = "Header",
            IsActive = true,
            Items =
            [
                new() { Id = Guid.NewGuid(), Label = "Home", Url = "/", DisplayOrder = 1, IsActive = true },
                new() { Id = Guid.NewGuid(), Label = "Products", Url = "/products", DisplayOrder = 2, IsActive = true },
                new() { Id = Guid.NewGuid(), Label = "About", Url = "/about", DisplayOrder = 3, IsActive = true },
                new() { Id = Guid.NewGuid(), Label = "Contact", Url = "/contact", DisplayOrder = 4, IsActive = true }
            ],
            CreatedAt = DateTime.UtcNow
        });
    }
}

public class GetSiteSettingsHandler : IRequestHandler<GetSiteSettingsQuery, ApiResponse<List<SiteSettingDto>>>
{
    public async Task<ApiResponse<List<SiteSettingDto>>> Handle(GetSiteSettingsQuery request, CancellationToken ct)
    {
        return ApiResponse<List<SiteSettingDto>>.SuccessResponse(
        [
            new() { Id = Guid.NewGuid(), Key = "SiteName", Value = "Ecommerce Store", Group = "General", IsPublic = true },
            new() { Id = Guid.NewGuid(), Key = "SiteTagline", Value = "Quality Products, Great Prices", Group = "General", IsPublic = true },
            new() { Id = Guid.NewGuid(), Key = "ContactEmail", Value = "support@example.com", Group = "Contact", IsPublic = true },
            new() { Id = Guid.NewGuid(), Key = "Currency", Value = "USD", Group = "Store", IsPublic = true },
            new() { Id = Guid.NewGuid(), Key = "TimeZone", Value = "UTC", Group = "Store", IsPublic = false }
        ]);
    }
}

public class UpdateSiteSettingHandler : IRequestHandler<UpdateSiteSettingCommand, ApiResponse<SiteSettingDto>>
{
    public async Task<ApiResponse<SiteSettingDto>> Handle(UpdateSiteSettingCommand request, CancellationToken ct)
    {
        return ApiResponse<SiteSettingDto>.SuccessResponse(new SiteSettingDto
        {
            Id = request.Id,
            Key = "UpdatedKey",
            Value = request.Request.Value,
            UpdatedAt = DateTime.UtcNow
        }, "Setting updated successfully");
    }
}

public class GetMediaFilesHandler : IRequestHandler<GetMediaFilesQuery, ApiResponse<List<MediaFileDto>>>
{
    public async Task<ApiResponse<List<MediaFileDto>>> Handle(GetMediaFilesQuery request, CancellationToken ct)
    {
        return ApiResponse<List<MediaFileDto>>.SuccessResponse([]);
    }
}

public class UploadMediaHandler : IRequestHandler<UploadMediaCommand, ApiResponse<MediaFileDto>>
{
    public async Task<ApiResponse<MediaFileDto>> Handle(UploadMediaCommand request, CancellationToken ct)
    {
        return ApiResponse<MediaFileDto>.SuccessResponse(new MediaFileDto
        {
            Id = Guid.NewGuid(),
            FileName = "uploaded-file.jpg",
            Url = "/uploads/uploaded-file.jpg",
            ContentType = "image/jpeg",
            FileSize = 1024000,
            Folder = request.Request.Folder ?? "default",
            CreatedAt = DateTime.UtcNow
        }, "File uploaded successfully");
    }
}

public class DeleteMediaHandler : IRequestHandler<DeleteMediaCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteMediaCommand request, CancellationToken ct)
    {
        return ApiResponse.SuccessResponse("File deleted successfully");
    }
}

public class GetCmsPageBySlugQuery : IRequest<ApiResponse<CmsPageDto>>
{
    public string Slug { get; set; } = string.Empty;
}

public class CreateCmsPageCommand : IRequest<ApiResponse<CmsPageDto>>
{
    public CreateCmsPageRequest Request { get; set; } = null!;
}

public class UpdateCmsPageCommand : IRequest<ApiResponse<CmsPageDto>>
{
    public Guid Id { get; set; }
    public UpdateCmsPageRequest Request { get; set; } = null!;
}

public class DeleteCmsPageCommand : IRequest<ApiResponse>
{
    public Guid Id { get; set; }
}

public class GetNavigationMenuQuery : IRequest<ApiResponse<NavigationMenuDto>>
{
    public string Position { get; set; } = "Header";
}

public class GetSiteSettingsQuery : IRequest<ApiResponse<List<SiteSettingDto>>>
{
    public string? Group { get; set; }
    public bool? IsPublic { get; set; }
}

public class UpdateSiteSettingCommand : IRequest<ApiResponse<SiteSettingDto>>
{
    public Guid Id { get; set; }
    public UpdateSiteSettingRequest Request { get; set; } = null!;
}

public class GetMediaFilesQuery : IRequest<ApiResponse<List<MediaFileDto>>>
{
    public string? Folder { get; set; }
    public string? ContentType { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class UploadMediaCommand : IRequest<ApiResponse<MediaFileDto>>
{
    public UploadMediaRequest Request { get; set; } = null!;
}

public class DeleteMediaCommand : IRequest<ApiResponse>
{
    public Guid Id { get; set; }
}
