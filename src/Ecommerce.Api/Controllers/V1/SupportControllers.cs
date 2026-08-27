using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger) => _logger = logger;

    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    public IActionResult GetProfile()
    {
        return Ok(ApiResponse.SuccessResponse("Profile endpoint"));
    }

    [HttpPut("profile")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public IActionResult UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        return Ok(ApiResponse.SuccessResponse("Profile updated successfully."));
    }

    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        return Ok(ApiResponse.SuccessResponse("Password changed successfully."));
    }

    [HttpGet("addresses")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<UserAddressDto>>), 200)]
    public IActionResult GetAddresses()
    {
        return Ok(ApiResponse<List<UserAddressDto>>.SuccessResponse([]));
    }

    [HttpPost("addresses")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserAddressDto>), 201)]
    public IActionResult CreateAddress([FromBody] CreateAddressRequest request)
    {
        return Ok(ApiResponse.SuccessResponse("Address created successfully."));
    }
}

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<CategoryDto>>), 200)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetActiveCategoriesAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), 201)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateCategoryAsync(request, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return StatusCode(201, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), 200)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, request, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteCategoryAsync(id, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MarketingController : ControllerBase
{
    private readonly ILogger<MarketingController> _logger;

    public MarketingController(ILogger<MarketingController> logger) => _logger = logger;

    [HttpPost("coupons/validate")]
    [ProducesResponseType(typeof(ApiResponse<ValidateCouponResponse>), 200)]
    public IActionResult ValidateCoupon([FromBody] ValidateCouponRequest request)
    {
        return Ok(ApiResponse<ValidateCouponResponse>.SuccessResponse(new ValidateCouponResponse
        {
            IsValid = true,
            CouponType = "Percentage",
            DiscountAmount = 10.00m
        }));
    }

    [HttpGet("banners")]
    [ProducesResponseType(typeof(ApiResponse<List<BannerDto>>), 200)]
    public IActionResult GetBanners([FromQuery] string position = "HomeTop")
    {
        return Ok(ApiResponse<List<BannerDto>>.SuccessResponse([]));
    }

    [HttpPost("newsletter/subscribe")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public IActionResult SubscribeNewsletter([FromBody] SubscribeNewsletterRequest request)
    {
        return Ok(ApiResponse.SuccessResponse("Successfully subscribed to newsletter."));
    }
}
