using Xunit;
using FluentAssertions;
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

namespace Ecommerce.UnitTests;

public class CmsPageDtoComprehensiveTests
{
    [Fact]
    public void CmsPageDto_AllProperties_ShouldBeSettable()
    {
        var dto = new CmsPageDto
        {
            Id = Guid.NewGuid(),
            Title = "About Us",
            Slug = "about-us",
            Content = "<p>Learn about our company</p>",
            MetaTitle = "About Us - Our Company",
            MetaDescription = "Learn about our company history",
            FeaturedImageUrl = "https://example.com/about.jpg",
            IsPublished = true,
            PublishedAt = DateTime.UtcNow,
            Author = "admin@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Title.Should().Be("About Us");
        dto.Slug.Should().Be("about-us");
        dto.IsPublished.Should().BeTrue();
    }

    [Fact]
    public void CmsPageDto_IsPublished_ShouldReturnTrueWhenPublished()
    {
        var dto = new CmsPageDto
        {
            IsPublished = true,
            PublishedAt = DateTime.UtcNow.AddDays(-1)
        };

        dto.IsPublished.Should().BeTrue();
    }

    [Fact]
    public void CmsPageDto_IsPublished_ShouldReturnFalseWhenNotPublished()
    {
        var dto = new CmsPageDto
        {
            IsPublished = false
        };

        dto.IsPublished.Should().BeFalse();
    }

    [Fact]
    public void CmsPageDto_HasSeo_ShouldReturnTrueWhenHasMetaTitle()
    {
        var dto = new CmsPageDto
        {
            MetaTitle = "About Us - Our Company"
        };

        dto.HasSeo.Should().BeTrue();
    }

    [Fact]
    public void CmsPageDto_HasSeo_ShouldReturnFalseWhenNoMetaTitle()
    {
        var dto = new CmsPageDto();

        dto.HasSeo.Should().BeFalse();
    }

    [Fact]
    public void CmsPageDto_ContentLength_ShouldReturnCorrectLength()
    {
        var dto = new CmsPageDto
        {
            Content = "<p>Learn about our company</p>"
        };

        dto.ContentLength.Should().Be(30);
    }
}

public class CreateCmsPageRequestComprehensiveTests
{
    [Fact]
    public void CreateCmsPageRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateCmsPageRequest
        {
            Title = "About Us",
            Slug = "about-us",
            Content = "<p>Learn about our company</p>",
            MetaTitle = "About Us - Our Company",
            MetaDescription = "Learn about our company history",
            FeaturedImageUrl = "https://example.com/about.jpg",
            IsPublished = true
        };

        request.Title.Should().Be("About Us");
        request.Slug.Should().Be("about-us");
        request.Content.Should().Be("<p>Learn about our company</p>");
    }
}

public class UpdateCmsPageRequestComprehensiveTests
{
    [Fact]
    public void UpdateCmsPageRequest_AllProperties_ShouldBeOptional()
    {
        var request = new UpdateCmsPageRequest();

        request.Title.Should().BeNull();
        request.Slug.Should().BeNull();
        request.Content.Should().BeNull();
        request.MetaTitle.Should().BeNull();
        request.MetaDescription.Should().BeNull();
        request.FeaturedImageUrl.Should().BeNull();
        request.IsPublished.Should().BeNull();
    }
}

public class CmsPageRevisionDtoComprehensiveTests
{
    [Fact]
    public void CmsPageRevisionDto_AllProperties_ShouldBeSettable()
    {
        var dto = new CmsPageRevisionDto
        {
            Id = Guid.NewGuid(),
            PageId = Guid.NewGuid(),
            Title = "About Us",
            Content = "<p>Learn about our company</p>",
            Version = 3,
            CreatedBy = "admin@example.com",
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Version.Should().Be(3);
        dto.CreatedBy.Should().Be("admin@example.com");
    }
}

public class NavigationMenuDtoComprehensiveTests
{
    [Fact]
    public void NavigationMenuDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NavigationMenuDto
        {
            Id = Guid.NewGuid(),
            Name = "Main Menu",
            Position = "Header",
            IsActive = true,
            Items =
            [
                new() { Id = Guid.NewGuid(), Label = "Home", Url = "/", DisplayOrder = 1, IsExternal = false },
                new() { Id = Guid.NewGuid(), Label = "Products", Url = "/products", DisplayOrder = 2, IsExternal = false },
                new() { Id = Guid.NewGuid(), Label = "About", Url = "/about", DisplayOrder = 3, IsExternal = false }
            ],
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Main Menu");
        dto.Position.Should().Be("Header");
        dto.IsActive.Should().BeTrue();
        dto.Items.Should().HaveCount(3);
    }

    [Fact]
    public void NavigationMenuDto_ActiveItemCount_ShouldCountActiveItems()
    {
        var dto = new NavigationMenuDto
        {
            Items =
            [
                new() { Id = Guid.NewGuid(), Label = "Home", Url = "/", DisplayOrder = 1, IsActive = true },
                new() { Id = Guid.NewGuid(), Label = "Products", Url = "/products", DisplayOrder = 2, IsActive = false },
                new() { Id = Guid.NewGuid(), Label = "About", Url = "/about", DisplayOrder = 3, IsActive = true }
            ]
        };

        dto.ActiveItemCount.Should().Be(2);
    }
}

public class NavigationMenuItemDtoComprehensiveTests
{
    [Fact]
    public void NavigationMenuItemDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NavigationMenuItemDto
        {
            Id = Guid.NewGuid(),
            Label = "Products",
            Url = "/products",
            Icon = "fas fa-shopping-bag",
            DisplayOrder = 2,
            IsExternal = false,
            IsActive = true,
            Children =
            [
                new() { Id = Guid.NewGuid(), Label = "Electronics", Url = "/products/electronics", DisplayOrder = 1 },
                new() { Id = Guid.NewGuid(), Label = "Clothing", Url = "/products/clothing", DisplayOrder = 2 }
            ]
        };

        dto.Id.Should().NotBeEmpty();
        dto.Label.Should().Be("Products");
        dto.Url.Should().Be("/products");
        dto.Children.Should().HaveCount(2);
    }

    [Fact]
    public void NavigationMenuItemDto_HasChildren_ShouldReturnTrueWhenHasChildren()
    {
        var dto = new NavigationMenuItemDto
        {
            Children =
            [
                new() { Id = Guid.NewGuid(), Label = "Child", Url = "/child" }
            ]
        };

        dto.HasChildren.Should().BeTrue();
    }

    [Fact]
    public void NavigationMenuItemDto_HasChildren_ShouldReturnFalseWhenNoChildren()
    {
        var dto = new NavigationMenuItemDto
        {
            Children = []
        };

        dto.HasChildren.Should().BeFalse();
    }

    [Fact]
    public void NavigationMenuItemDto_IsExternalLink_ShouldReturnTrueWhenExternal()
    {
        var dto = new NavigationMenuItemDto
        {
            Url = "https://external-site.com",
            IsExternal = true
        };

        dto.IsExternalLink.Should().BeTrue();
    }

    [Fact]
    public void NavigationMenuItemDto_IsExternalLink_ShouldReturnFalseWhenInternal()
    {
        var dto = new NavigationMenuItemDto
        {
            Url = "/products",
            IsExternal = false
        };

        dto.IsExternalLink.Should().BeFalse();
    }
}

public class SiteSettingDtoComprehensiveTests
{
    [Fact]
    public void SiteSettingDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SiteSettingDto
        {
            Id = Guid.NewGuid(),
            Key = "site.name",
            Value = "My Ecommerce Store",
            Group = "General",
            Description = "The site name",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Key.Should().Be("site.name");
        dto.Value.Should().Be("My Ecommerce Store");
        dto.Group.Should().Be("General");
        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void SiteSettingDto_GetTypedValue_ShouldReturnTypedValue()
    {
        var dto = new SiteSettingDto
        {
            Key = "site.name",
            Value = "My Store"
        };

        var value = dto.GetTypedValue<string>();

        value.Should().Be("My Store");
    }
}

public class MediaFileDtoComprehensiveTests
{
    [Fact]
    public void MediaFileDto_AllProperties_ShouldBeSettable()
    {
        var dto = new MediaFileDto
        {
            Id = Guid.NewGuid(),
            FileName = "product-image.jpg",
            OriginalFileName = "My Product Image.jpg",
            ContentType = "image/jpeg",
            Url = "https://storage.example.com/media/product-image.jpg",
            ThumbnailUrl = "https://storage.example.com/media/thumbnails/product-image.jpg",
            FileSize = 1024000,
            Width = 1920,
            Height = 1080,
            AltText = "Product image",
            Folder = "products",
            Tags = ["product", "main-image"],
            CreatedAt = DateTime.UtcNow,
            UploadedBy = "admin@example.com"
        };

        dto.Id.Should().NotBeEmpty();
        dto.FileName.Should().Be("product-image.jpg");
        dto.ContentType.Should().Be("image/jpeg");
        dto.FileSize.Should().Be(1024000);
        dto.Width.Should().Be(1920);
        dto.Height.Should().Be(1080);
        dto.AltText.Should().Be("Product image");
    }

    [Fact]
    public void MediaFileDto_IsImage_ShouldReturnTrueForImageContentType()
    {
        var dto = new MediaFileDto { ContentType = "image/jpeg" };

        dto.IsImage.Should().BeTrue();
    }

    [Fact]
    public void MediaFileDto_IsImage_ShouldReturnFalseForNonImageContentType()
    {
        var dto = new MediaFileDto { ContentType = "application/pdf" };

        dto.IsImage.Should().BeFalse();
    }

    [Fact]
    public void MediaFileDto_IsVideo_ShouldReturnTrueForVideoContentType()
    {
        var dto = new MediaFileDto { ContentType = "video/mp4" };

        dto.IsVideo.Should().BeTrue();
    }

    [Fact]
    public void MediaFileDto_IsVideo_ShouldReturnFalseForNonVideoContentType()
    {
        var dto = new MediaFileDto { ContentType = "image/jpeg" };

        dto.IsVideo.Should().BeFalse();
    }

    [Fact]
    public void MediaFileDto_FileSizeFormatted_ShouldReturnCorrectFormat()
    {
        var dto = new MediaFileDto { FileSize = 1024000 };

        dto.FileSizeFormatted.Should().Be("1.00 MB");
    }

    [Fact]
    public void MediaFileDto_Dimensions_ShouldReturnCorrectDimensions()
    {
        var dto = new MediaFileDto
        {
            Width = 1920,
            Height = 1080
        };

        dto.Dimensions.Should().Be("1920x1080");
    }

    [Fact]
    public void MediaFileDto_Dimensions_ShouldReturnEmptyWhenNoDimensions()
    {
        var dto = new MediaFileDto();

        dto.Dimensions.Should().BeEmpty();
    }
}

public class UploadMediaRequestComprehensiveTests
{
    [Fact]
    public void UploadMediaRequest_AllProperties_ShouldBeSettable()
    {
        var request = new UploadMediaRequest
        {
            Folder = "products",
            AltText = "Product image",
            Tags = ["product", "main-image"]
        };

        request.Folder.Should().Be("products");
        request.AltText.Should().Be("Product image");
        request.Tags.Should().HaveCount(2);
    }
}

public class UpdateMediaRequestComprehensiveTests
{
    [Fact]
    public void UpdateMediaRequest_AllProperties_ShouldBeOptional()
    {
        var request = new UpdateMediaRequest();

        request.AltText.Should().BeNull();
        request.Folder.Should().BeNull();
        request.Tags.Should().BeNull();
    }
}
