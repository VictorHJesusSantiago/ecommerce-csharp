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

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, ApiResponse<List<CategoryDto>>>
{
    private readonly ICategoryService _categoryService;
    public GetCategoriesHandler(ICategoryService categoryService) => _categoryService = categoryService;
    public async Task<ApiResponse<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken ct)
    {
        return await _categoryService.GetAllCategoriesAsync(ct);
    }
}

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryDto>>
{
    private readonly ICategoryService _categoryService;
    public GetCategoryByIdHandler(ICategoryService categoryService) => _categoryService = categoryService;
    public async Task<ApiResponse<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        return await _categoryService.GetCategoryByIdAsync(request.Id, ct);
    }
}

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryDto>>
{
    private readonly ICategoryService _categoryService;
    public CreateCategoryHandler(ICategoryService categoryService) => _categoryService = categoryService;
    public async Task<ApiResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        return await _categoryService.CreateCategoryAsync(request.Request, ct);
    }
}

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<CategoryDto>>
{
    private readonly ICategoryService _categoryService;
    public UpdateCategoryHandler(ICategoryService categoryService) => _categoryService = categoryService;
    public async Task<ApiResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken ct)
    {
        return await _categoryService.UpdateCategoryAsync(request.Id, request.Request, ct);
    }
}

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse>
{
    private readonly ICategoryService _categoryService;
    public DeleteCategoryHandler(ICategoryService categoryService) => _categoryService = categoryService;
    public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        return await _categoryService.DeleteCategoryAsync(request.Id, ct);
    }
}

public class GetCategoryTreeHandler : IRequestHandler<GetCategoryTreeQuery, ApiResponse<List<CategoryDto>>>
{
    private readonly ICategoryService _categoryService;
    public GetCategoryTreeHandler(ICategoryService categoryService) => _categoryService = categoryService;
    public async Task<ApiResponse<List<CategoryDto>>> Handle(GetCategoryTreeQuery request, CancellationToken ct)
    {
        return await _categoryService.GetCategoryTreeAsync(ct);
    }
}

public class GetCategoriesQuery : IRequest<ApiResponse<List<CategoryDto>>>
{
    public bool? IsActive { get; set; }
    public Guid? ParentCategoryId { get; set; }
}

public class GetCategoryByIdQuery : IRequest<ApiResponse<CategoryDto>>
{
    public Guid Id { get; set; }
}

public class CreateCategoryCommand : IRequest<ApiResponse<CategoryDto>>
{
    public CreateCategoryRequest Request { get; set; } = null!;
}

public class UpdateCategoryCommand : IRequest<ApiResponse<CategoryDto>>
{
    public Guid Id { get; set; }
    public UpdateCategoryRequest Request { get; set; } = null!;
}

public class DeleteCategoryCommand : IRequest<ApiResponse>
{
    public Guid Id { get; set; }
}

public class GetCategoryTreeQuery : IRequest<ApiResponse<List<CategoryDto>>>
{
    public bool? IsActive { get; set; }
}
