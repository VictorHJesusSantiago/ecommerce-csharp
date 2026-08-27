using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Payment;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.User;
using Ecommerce.Domain.Entities.Marketing;
using Ecommerce.Domain.Entities.Inventory;
using Ecommerce.Domain.Entities.Notification;
using Ecommerce.Domain.Entities.Cms;
using Ecommerce.Domain.Entities.Shipping;

namespace Ecommerce.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductListDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null));
        CreateMap<ProductImage, ProductImageDto>();
        CreateMap<ProductVariant, ProductVariantDto>();
    }
}

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null));
    }
}

public class BrandMappingProfile : Profile
{
    public BrandMappingProfile()
    {
        CreateMap<Brand, BrandDto>();
    }
}

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderHistory, OrderHistoryDto>();
    }
}

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<Address, UserAddressDto>();
        CreateMap<PaymentMethod, PaymentMethodDto>();
        CreateMap<Wishlist, WishlistDto>();
        CreateMap<Wishlist, WishlistItemDto>();
        CreateMap<UserActivity, UserActivityDto>();
    }
}

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<ShoppingCart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
    }
}

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<PaymentRecord, PaymentDto>();
        CreateMap<RefundRecord, RefundDto>();
    }
}

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<ProductReview, ReviewDto>();
        CreateMap<ReviewImage, ReviewImageDto>();
    }
}

public class MarketingMappingProfile : Profile
{
    public MarketingMappingProfile()
    {
        CreateMap<Coupon, CouponDto>();
        CreateMap<Promotion, PromotionDto>();
        CreateMap<Banner, BannerDto>();
        CreateMap<NewsletterSubscriber, NewsletterSubscriberDto>();
        CreateMap<Discount, DiscountDto>();
    }
}

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<WarehouseInventory, InventoryItemDto>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<WarehouseInventoryMovement, InventoryMovementDto>();
    }
}

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<NotificationRecord, NotificationDto>();
        CreateMap<EmailTemplate, EmailTemplateDto>();
    }
}

public class CmsMappingProfile : Profile
{
    public CmsMappingProfile()
    {
        CreateMap<CmsPage, CmsPageDto>();
        CreateMap<CmsPageRevision, CmsPageRevisionDto>();
        CreateMap<NavigationMenu, NavigationMenuDto>();
        CreateMap<NavigationMenuItem, NavigationMenuItemDto>();
        CreateMap<SiteSetting, SiteSettingDto>();
        CreateMap<MediaFile, MediaFileDto>();
    }
}
