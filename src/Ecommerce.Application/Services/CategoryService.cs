using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepo, ILogger<CategoryService> logger)
    {
        _categoryRepo = categoryRepo;
        _logger = logger;
    }

    public async Task<ApiResponse<List<CategoryDto>>> GetActiveCategoriesAsync(CancellationToken ct = default)
    {
        var categories = await _categoryRepo.FindAsync(c => c.IsActive, ct);
        var dtos = categories.OrderBy(c => c.SortOrder).Select(MapToDto).ToList();
        return ApiResponse<List<CategoryDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id, CancellationToken ct = default)
    {
        var category = await _categoryRepo.GetByIdAsync(id, ct);
        if (category is null)
            return ApiResponse<CategoryDto>.FailResponse("Category not found.", 404);
        return ApiResponse<CategoryDto>.SuccessResponse(MapToDto(category));
    }

    public async Task<ApiResponse<CategoryDto>> GetCategoryBySlugAsync(string slug, CancellationToken ct = default)
    {
        var category = await _categoryRepo.GetBySlugAsync(slug, ct);
        if (category is null)
            return ApiResponse<CategoryDto>.FailResponse("Category not found.", 404);
        return ApiResponse<CategoryDto>.SuccessResponse(MapToDto(category));
    }

    public async Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct = default)
    {
        var category = new Ecommerce.Domain.Entities.Catalog.Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Slug = request.Name.ToLower().Replace(" ", "-"),
            ImageUrl = request.ImageUrl,
            ParentId = request.ParentId,
            SortOrder = request.SortOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _categoryRepo.AddAsync(category, ct);
        _logger.LogInformation("Category created: {CategoryName} ({CategoryId})", category.Name, category.Id);

        return ApiResponse<CategoryDto>.SuccessResponse(MapToDto(category));
    }

    public async Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default)
    {
        var category = await _categoryRepo.GetByIdAsync(id, ct);
        if (category is null)
            return ApiResponse<CategoryDto>.FailResponse("Category not found.", 404);

        category.Name = request.Name ?? category.Name;
        category.Description = request.Description ?? category.Description;
        category.ImageUrl = request.ImageUrl ?? category.ImageUrl;

        await _categoryRepo.UpdateAsync(category, ct);
        _logger.LogInformation("Category updated: {CategoryId}", id);

        return ApiResponse<CategoryDto>.SuccessResponse(MapToDto(category));
    }

    public async Task<ApiResponse> DeleteCategoryAsync(Guid id, CancellationToken ct = default)
    {
        var category = await _categoryRepo.GetByIdAsync(id, ct);
        if (category is null)
            return ApiResponse.FailResponse("Category not found.", 404);

        category.IsActive = false;
        await _categoryRepo.UpdateAsync(category, ct);
        _logger.LogInformation("Category deleted: {CategoryId}", id);

        return ApiResponse.SuccessResponse("Category deleted successfully.");
    }

    public async Task<ApiResponse<int>> GetCategoryProductCountAsync(Guid categoryId, CancellationToken ct = default)
    {
        var category = await _categoryRepo.GetByIdAsync(categoryId, ct);
        return ApiResponse<int>.SuccessResponse(category?.Products?.Count ?? 0);
    }

    private static CategoryDto MapToDto(Ecommerce.Domain.Entities.Catalog.Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description,
        Slug = c.Slug,
        ImageUrl = c.ImageUrl,
        ParentId = c.ParentId,
        SortOrder = c.SortOrder,
        IsActive = c.IsActive,
        ProductCount = c.Products?.Count ?? 0
    };
}
