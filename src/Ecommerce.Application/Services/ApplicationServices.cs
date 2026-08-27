using MediatR;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService,
        IEventBus eventBus,
        ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<ApiResponse<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"product:{id}";
        var cachedProduct = await _cacheService.GetAsync<ProductDto>(cacheKey, cancellationToken);
        if (cachedProduct is not null)
            return ApiResponse<ProductDto>.SuccessResponse(cachedProduct);

        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return ApiResponse<ProductDto>.FailResponse("Product not found.", 404);

        var productDto = _mapper.Map<ProductDto>(product);
        await _cacheService.SetAsync(cacheKey, productDto, TimeSpan.FromMinutes(15), cancellationToken);

        return ApiResponse<ProductDto>.SuccessResponse(productDto);
    }

    public async Task<ApiResponse<ProductDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"product:slug:{slug}";
        var cachedProduct = await _cacheService.GetAsync<ProductDto>(cacheKey, cancellationToken);
        if (cachedProduct is not null)
            return ApiResponse<ProductDto>.SuccessResponse(cachedProduct);

        var product = await _unitOfWork.Products.GetBySlugAsync(slug, cancellationToken);
        if (product is null)
            return ApiResponse<ProductDto>.FailResponse("Product not found.", 404);

        product.IncrementViewCount();
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = _mapper.Map<ProductDto>(product);
        await _cacheService.SetAsync(cacheKey, productDto, TimeSpan.FromMinutes(15), cancellationToken);

        return ApiResponse<ProductDto>.SuccessResponse(productDto);
    }

    public async Task<ApiResponse<PagedResponse<ProductListDto>>> SearchProductsAsync(
        ProductSearchRequest request, CancellationToken cancellationToken = default)
    {
        var (products, totalCount) = await _unitOfWork.Products.GetPagedAsync(
            request.Page, request.PageSize, request.CategoryId, request.BrandId,
            request.MinPrice, request.MaxPrice, request.SortBy, request.SortDescending,
            request.SearchTerm, cancellationToken);

        var productDtos = _mapper.Map<List<ProductListDto>>(products);
        var pagedResponse = PagedResponse<ProductListDto>.Create(productDtos, totalCount, request.Page, request.PageSize);

        return ApiResponse<PagedResponse<ProductListDto>>.SuccessResponse(pagedResponse);
    }

    public async Task<ApiResponse<ProductDto>> CreateProductAsync(
        CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var existingSku = await _unitOfWork.Products.GetBySkuAsync(request.SKU, cancellationToken);
        if (existingSku is not null)
            return ApiResponse<ProductDto>.FailResponse($"A product with SKU '{request.SKU}' already exists.", 409);

        var product = Product.Create(
            request.Name, request.SKU, request.Price, request.CategoryId,
            request.Description, request.ShortDescription,
            Enums.ProductType.Physical, request.Currency, request.TaxRate,
            request.IsTaxable, request.IsShippingRequired, request.Weight,
            request.WeightUnit, request.LowStockThreshold, request.AllowReviews);

        foreach (var variantRequest in request.Variants)
        {
            var variant = ProductVariant.Create(
                variantRequest.Name, variantRequest.SKU, variantRequest.Price, product.Id,
                variantRequest.CompareAtPrice, variantRequest.CostPrice, variantRequest.Weight,
                variantRequest.WeightUnit, variantRequest.StockQuantity, variantRequest.IsDefault,
                variantRequest.Option1, variantRequest.Option2, variantRequest.Option3,
                variantRequest.ImageUrl);
            product.AddVariant(variant);
        }

        product.Update(
            request.Name, request.Description, request.ShortDescription,
            request.CompareAtPrice, request.CostPrice, request.TaxRate,
            request.IsTaxable, request.IsShippingRequired, request.Weight,
            request.WeightUnit, request.IsFeatured, isNewArrival: false,
            isBestSeller: false, request.AllowReviews, request.AllowBackorder,
            lowStockThreshold: request.LowStockThreshold,
            metaTitle: request.MetaTitle, metaDescription: request.MetaDescription,
            metaKeywords: request.MetaKeywords, tags: request.Tags);

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product {ProductId} created: {ProductName}", product.Id, product.Name);
        await _eventBus.PublishAsync(new Domain.Events.Catalog.ProductCreatedEvent(
            product.Id, product.Name, product.Price, 0), cancellationToken);

        var productDto = _mapper.Map<ProductDto>(product);
        await _cacheService.RemoveByPatternAsync("products:*", cancellationToken);

        return ApiResponse<ProductDto>.SuccessResponse(productDto, "Product created successfully.", 201);
    }

    public async Task<ApiResponse<ProductDto>> UpdateProductAsync(
        Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return ApiResponse<ProductDto>.FailResponse("Product not found.", 404);

        product.Update(
            request.Name, request.Description, request.ShortDescription,
            request.CompareAtPrice, request.CostPrice, request.TaxRate,
            request.IsTaxable, request.IsShippingRequired, request.Weight,
            request.WeightUnit, request.IsFeatured, request.IsNewArrival,
            request.IsBestSeller, request.AllowReviews, request.AllowBackorder,
            request.MinOrderQuantity, request.MaxOrderQuantity,
            request.LowStockThreshold, request.MetaTitle,
            request.MetaDescription, request.MetaKeywords, request.Tags);

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product {ProductId} updated", product.Id);

        await _cacheService.RemoveAsync($"product:{id}", cancellationToken);
        await _cacheService.RemoveByPatternAsync("products:*", cancellationToken);

        var productDto = _mapper.Map<ProductDto>(product);
        return ApiResponse<ProductDto>.SuccessResponse(productDto, "Product updated successfully.");
    }

    public async Task<ApiResponse> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return ApiResponse.FailResponse("Product not found.", 404);

        product.MarkAsDeleted();
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product {ProductId} deleted", product.Id);
        await _eventBus.PublishAsync(new Domain.Events.Catalog.ProductDeletedEvent(id, product.Name), cancellationToken);

        await _cacheService.RemoveAsync($"product:{id}", cancellationToken);
        await _cacheService.RemoveByPatternAsync("products:*", cancellationToken);

        return ApiResponse.SuccessResponse("Product deleted successfully.");
    }

    public async Task<ApiResponse> PublishProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return ApiResponse.FailResponse("Product not found.", 404);

        product.Publish();
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync($"product:{id}", cancellationToken);
        await _cacheService.RemoveByPatternAsync("products:*", cancellationToken);

        return ApiResponse.SuccessResponse("Product published successfully.");
    }

    public async Task<ApiResponse<List<ProductDto>>> GetFeaturedProductsAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"products:featured:{count}";
        var cached = await _cacheService.GetAsync<List<ProductDto>>(cacheKey, cancellationToken);
        if (cached is not null)
            return ApiResponse<List<ProductDto>>.SuccessResponse(cached);

        var products = await _unitOfWork.Products.GetFeaturedAsync(count, cancellationToken);
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        await _cacheService.SetAsync(cacheKey, productDtos, TimeSpan.FromMinutes(30), cancellationToken);
        return ApiResponse<List<ProductDto>>.SuccessResponse(productDtos);
    }

    public async Task<ApiResponse<List<ProductDto>>> GetNewArrivalsAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        var products = await _unitOfWork.Products.GetNewArrivalsAsync(count, cancellationToken);
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        return ApiResponse<List<ProductDto>>.SuccessResponse(productDtos);
    }

    public async Task<ApiResponse<List<ProductDto>>> GetBestSellersAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"products:bestsellers:{count}";
        var cached = await _cacheService.GetAsync<List<ProductDto>>(cacheKey, cancellationToken);
        if (cached is not null)
            return ApiResponse<List<ProductDto>>.SuccessResponse(cached);

        var products = await _unitOfWork.Products.GetBestSellersAsync(count, cancellationToken);
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        await _cacheService.SetAsync(cacheKey, productDtos, TimeSpan.FromMinutes(30), cancellationToken);
        return ApiResponse<List<ProductDto>>.SuccessResponse(productDtos);
    }

    public async Task<ApiResponse<int>> GetStockQuantityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(productId, cancellationToken);
        if (product is null)
            return ApiResponse<int>.FailResponse("Product not found.", 404);

        var totalStock = product.Variants.Sum(v => v.StockQuantity);
        return ApiResponse<int>.SuccessResponse(totalStock);
    }
}

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, ILogger<CategoryService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<ApiResponse<List<CategoryDto>>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = "categories:active";
        var cached = await _cacheService.GetAsync<List<CategoryDto>>(cacheKey, cancellationToken);
        if (cached is not null)
            return ApiResponse<List<CategoryDto>>.SuccessResponse(cached);

        var categories = await _unitOfWork.Categories.GetActiveCategoriesAsync(cancellationToken);
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

        await _cacheService.SetAsync(cacheKey, categoryDtos, TimeSpan.FromHours(1), cancellationToken);
        return ApiResponse<List<CategoryDto>>.SuccessResponse(categoryDtos);
    }

    public async Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category is null)
            return ApiResponse<CategoryDto>.FailResponse("Category not found.", 404);

        return ApiResponse<CategoryDto>.SuccessResponse(_mapper.Map<CategoryDto>(category));
    }

    public async Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = Category.Create(request.Name, slug: null, request.Description,
            request.ImageUrl, request.ParentCategoryId, request.DisplayOrder);

        await _unitOfWork.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Category {CategoryId} created: {CategoryName}", category.Id, category.Name);
        await _cacheService.RemoveByPatternAsync("categories:*", cancellationToken);

        return ApiResponse<CategoryDto>.SuccessResponse(
            _mapper.Map<CategoryDto>(category), "Category created successfully.", 201);
    }

    public async Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category is null)
            return ApiResponse<CategoryDto>.FailResponse("Category not found.", 404);

        category.Update(request.Name, request.Description, request.ImageUrl,
            request.DisplayOrder, request.IsActive, request.IsFeatured,
            request.MetaTitle, request.MetaDescription, request.MetaKeywords);

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveByPatternAsync("categories:*", cancellationToken);

        return ApiResponse<CategoryDto>.SuccessResponse(
            _mapper.Map<CategoryDto>(category), "Category updated successfully.");
    }

    public async Task<ApiResponse> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category is null)
            return ApiResponse.FailResponse("Category not found.", 404);

        if (category.Products.Any(p => !p.IsDeleted))
            return ApiResponse.FailResponse("Cannot delete category with products.");

        category.MarkAsDeleted();
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveByPatternAsync("categories:*", cancellationToken);
        return ApiResponse.SuccessResponse("Category deleted successfully.");
    }
}

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CartService> _logger;

    public CartService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, ILogger<CartService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<ApiResponse<CartDto>> GetCartAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default)
    {
        var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(userId, sessionId, cancellationToken);
        if (cart is null)
        {
            cart = userId.HasValue
                ? Domain.Entities.Cart.ShoppingCart.CreateForUser(userId.Value)
                : Domain.Entities.Cart.ShoppingCart.CreateForSession(sessionId ?? Guid.NewGuid().ToString("N"));
            await _unitOfWork.Carts.AddAsync(cart, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return ApiResponse<CartDto>.SuccessResponse(_mapper.Map<CartDto>(cart));
    }

    public async Task<ApiResponse<CartDto>> AddToCartAsync(Guid? userId, string? sessionId, AddToCartRequest request, CancellationToken cancellationToken = default)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return ApiResponse<CartDto>.FailResponse("Product not found.", 404);

        if (product.Status != Enums.ProductStatus.Active)
            return ApiResponse<CartDto>.FailResponse("Product is not available.");

        var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(userId, sessionId, cancellationToken);
        if (cart is null)
        {
            cart = userId.HasValue
                ? Domain.Entities.Cart.ShoppingCart.CreateForUser(userId.Value)
                : Domain.Entities.Cart.ShoppingCart.CreateForSession(sessionId ?? Guid.NewGuid().ToString("N"));
            await _unitOfWork.Carts.AddAsync(cart, cancellationToken);
        }

        var primaryImage = product.GetPrimaryImageUrl();
        cart.AddItem(
            product.Id, product.Name, product.Slug, primaryImage,
            request.Quantity, product.Price,
            request.VariantId, null, product.SKU, request.Options);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Item added to cart: {ProductName} x{Quantity}", product.Name, request.Quantity);

        return ApiResponse<CartDto>.SuccessResponse(
            _mapper.Map<CartDto>(cart), "Item added to cart.");
    }

    public async Task<ApiResponse> RemoveFromCartAsync(Guid? userId, string? sessionId, Guid productId, string? variantId, CancellationToken cancellationToken = default)
    {
        var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(userId, sessionId, cancellationToken);
        if (cart is null)
            return ApiResponse.FailResponse("Cart not found.", 404);

        cart.RemoveItem(productId, variantId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.SuccessResponse("Item removed from cart.");
    }

    public async Task<ApiResponse> UpdateCartItemAsync(Guid? userId, string? sessionId, Guid productId, UpdateCartItemRequest request, string? variantId = null, CancellationToken cancellationToken = default)
    {
        var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(userId, sessionId, cancellationToken);
        if (cart is null)
            return ApiResponse.FailResponse("Cart not found.", 404);

        cart.UpdateItemQuantity(productId, request.Quantity, variantId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.SuccessResponse("Cart updated.");
    }

    public async Task<ApiResponse<CartDto>> ClearCartAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default)
    {
        var cart = await _unitOfWork.Carts.GetCartWithItemsAsync(userId, sessionId, cancellationToken);
        if (cart is null)
            return ApiResponse<CartDto>.FailResponse("Cart not found.", 404);

        cart.ClearItems();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CartDto>.SuccessResponse(_mapper.Map<CartDto>(cart), "Cart cleared.");
    }
}

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus, ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _unitOfWork.Orders.GetWithItemsAsync(id, cancellationToken);
        if (order is null)
            return ApiResponse<OrderDto>.FailResponse("Order not found.", 404);

        return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(order));
    }

    public async Task<ApiResponse<OrderDto>> GetOrderByNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
    {
        var order = await _unitOfWork.Orders.GetByOrderNumberAsync(orderNumber, cancellationToken);
        if (order is null)
            return ApiResponse<OrderDto>.FailResponse("Order not found.", 404);

        return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(order));
    }

    public async Task<ApiResponse<PagedResponse<OrderDto>>> GetOrdersAsync(OrderSearchRequest request, CancellationToken cancellationToken = default)
    {
        var (orders, totalCount) = await _unitOfWork.Orders.GetPagedAsync(
            request.Page, request.PageSize, request.Status, request.CustomerId,
            request.FromDate, request.ToDate, request.SearchTerm, cancellationToken);

        var orderDtos = _mapper.Map<List<OrderDto>>(orders);
        var pagedResponse = PagedResponse<OrderDto>.Create(orderDtos, totalCount, request.Page, request.PageSize);

        return ApiResponse<PagedResponse<OrderDto>>.SuccessResponse(pagedResponse);
    }

    public async Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusRequest request, string? performedBy = null, CancellationToken cancellationToken = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
        if (order is null)
            return ApiResponse<OrderDto>.FailResponse("Order not found.", 404);

        var oldStatus = order.Status.ToString();

        switch (request.Status)
        {
            case "Confirmed": order.Confirm(performedBy); break;
            case "Processing": order.StartProcessing(performedBy); break;
            case "Shipped": order.Ship(performedBy); break;
            case "Delivered": order.MarkDelivered(performedBy); break;
            case "OnHold": order.PlaceOnHold(request.Notes, performedBy); break;
            default:
                return ApiResponse<OrderDto>.FailResponse($"Cannot transition to status '{request.Status}'.");
        }

        if (!string.IsNullOrEmpty(request.Notes))
            order.AddNote(request.Notes, performedBy);

        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Order {OrderNumber} status changed from {OldStatus} to {NewStatus}",
            order.OrderNumber, oldStatus, request.Status);

        await _eventBus.PublishAsync(new Domain.Events.Ordering.OrderStatusChangedEvent(
            order.Id, order.OrderNumber, oldStatus, request.Status), cancellationToken);

        return ApiResponse<OrderDto>.SuccessResponse(
            _mapper.Map<OrderDto>(order), "Order status updated successfully.");
    }

    public async Task<ApiResponse> CancelOrderAsync(Guid id, string reason, string? cancelledBy = null, CancellationToken cancellationToken = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
        if (order is null)
            return ApiResponse.FailResponse("Order not found.", 404);

        try
        {
            order.Cancel(reason, cancelledBy);
        }
        catch (Domain.Exceptions.InvalidDomainOperationException ex)
        {
            return ApiResponse.FailResponse(ex.Message);
        }

        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventBus.PublishAsync(new Domain.Events.Ordering.OrderCancelledEvent(
            order.Id, order.OrderNumber, reason), cancellationToken);

        return ApiResponse.SuccessResponse("Order cancelled successfully.");
    }

    public async Task<ApiResponse<List<OrderDto>>> GetCustomerOrdersAsync(Guid customerId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var orders = await _unitOfWork.Orders.GetByCustomerAsync(customerId, cancellationToken);
        var pagedOrders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var orderDtos = _mapper.Map<List<OrderDto>>(pagedOrders);

        return ApiResponse<List<OrderDto>>.SuccessResponse(orderDtos);
    }
}

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReviewService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponse<List<DTOs.Review.ReviewDto>>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var reviews = await _unitOfWork.Reviews.GetByProductIdAsync(productId, cancellationToken);
        var reviewDtos = _mapper.Map<List<DTOs.Review.ReviewDto>>(reviews);
        return ApiResponse<List<DTOs.Review.ReviewDto>>.SuccessResponse(reviewDtos);
    }

    public async Task<ApiResponse<DTOs.Review.ReviewDto>> CreateReviewAsync(Guid userId, CreateReviewRequest request, CancellationToken cancellationToken = default)
    {
        var review = ProductReview.Create(
            request.ProductId, userId, request.Title, request.Body,
            request.Rating, request.Pros, request.Cons,
            isVerifiedPurchase: request.OrderId.HasValue, request.OrderId);

        await _unitOfWork.Reviews.AddAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Review created for product {ProductId} by user {UserId}", request.ProductId, userId);

        return ApiResponse<DTOs.Review.ReviewDto>.SuccessResponse(
            _mapper.Map<DTOs.Review.ReviewDto>(review), "Review submitted for approval.", 201);
    }

    public async Task<ApiResponse<DTOs.Review.ReviewDto>> ApproveReviewAsync(Guid reviewId, CancellationToken cancellationToken = default)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId, cancellationToken);
        if (review is null)
            return ApiResponse<DTOs.Review.ReviewDto>.FailResponse("Review not found.", 404);

        review.Approve();
        _unitOfWork.Reviews.Update(review);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var product = await _unitOfWork.Products.GetByIdAsync(review.ProductId, cancellationToken);
        if (product is not null)
        {
            var avgRating = await _unitOfWork.Reviews.GetAverageRatingAsync(review.ProductId, cancellationToken);
            var reviewCount = await _unitOfWork.Reviews.GetReviewCountAsync(review.ProductId, cancellationToken);
            product.UpdateRating(avgRating, reviewCount);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return ApiResponse<DTOs.Review.ReviewDto>.SuccessResponse(
            _mapper.Map<DTOs.Review.ReviewDto>(review), "Review approved.");
    }

    public async Task<ApiResponse> VoteReviewAsync(Guid reviewId, Guid userId, bool isHelpful, CancellationToken cancellationToken = default)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId, cancellationToken);
        if (review is null)
            return ApiResponse.FailResponse("Review not found.", 404);

        var voted = review.Vote(userId, isHelpful);
        if (!voted)
            return ApiResponse.FailResponse("You have already voted on this review.");

        _unitOfWork.Reviews.Update(review);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.SuccessResponse("Vote recorded.");
    }
}
