using MediatR;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.Product;
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

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductDto>>
{
    private readonly IProductService _service;
    public GetProductByIdHandler(IProductService service) => _service = service;
    public async Task<ApiResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        return await _service.GetByIdAsync(request.Id, ct);
    }
}

public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugQuery, ApiResponse<ProductDto>>
{
    private readonly IProductService _service;
    public GetProductBySlugHandler(IProductService service) => _service = service;
    public async Task<ApiResponse<ProductDto>> Handle(GetProductBySlugQuery request, CancellationToken ct)
    {
        return await _service.GetBySlugAsync(request.Slug, ct);
    }
}

public class SearchProductsHandler : IRequestHandler<SearchProductsQuery, ApiResponse<PagedResponse<ProductListDto>>>
{
    private readonly IProductService _service;
    public SearchProductsHandler(IProductService service) => _service = service;
    public async Task<ApiResponse<PagedResponse<ProductListDto>>> Handle(SearchProductsQuery request, CancellationToken ct)
    {
        var searchRequest = new ProductSearchRequest
        {
            SearchQuery = request.SearchQuery,
            Page = request.Page,
            PageSize = request.PageSize,
            CategoryId = request.CategoryId,
            BrandId = request.BrandId,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            SortBy = request.SortBy
        };
        return await _service.SearchProductsAsync(searchRequest, ct);
    }
}

public class GetFeaturedProductsHandler : IRequestHandler<GetFeaturedProductsQuery, ApiResponse<List<ProductDto>>>
{
    private readonly IProductService _service;
    public GetFeaturedProductsHandler(IProductService service) => _service = service;
    public async Task<ApiResponse<List<ProductDto>>> Handle(GetFeaturedProductsQuery request, CancellationToken ct)
    {
        return await _service.GetFeaturedProductsAsync(request.Count, ct);
    }
}

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductDto>>
{
    private readonly IProductService _service;
    public CreateProductHandler(IProductService service) => _service = service;
    public async Task<ApiResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var createRequest = new CreateProductRequest
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Sku = request.Sku,
            CategoryId = request.CategoryId,
            BrandId = request.BrandId
        };
        return await _service.CreateProductAsync(createRequest, ct);
    }
}

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductDto>>
{
    private readonly IProductService _service;
    public UpdateProductHandler(IProductService service) => _service = service;
    public async Task<ApiResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var updateRequest = new UpdateProductRequest
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };
        return await _service.UpdateProductAsync(request.Id, updateRequest, ct);
    }
}

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
{
    private readonly IProductService _service;
    public DeleteProductHandler(IProductService service) => _service = service;
    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        return await _service.DeleteProductAsync(request.Id, ct);
    }
}
