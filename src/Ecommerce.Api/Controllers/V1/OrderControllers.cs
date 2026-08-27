using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<OrderDto>>), 200)]
    public async Task<IActionResult> GetOrders([FromQuery] OrderSearchRequest request, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrdersAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrderByIdAsync(id, cancellationToken);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpGet("number/{orderNumber}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOrderByNumber(string orderNumber, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrderByNumberAsync(orderNumber, cancellationToken);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpGet("my-orders")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<OrderDto>>), 200)]
    public async Task<IActionResult> GetMyOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await _orderService.GetCustomerOrdersAsync(userId, page, pageSize, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var result = await _orderService.UpdateOrderStatusAsync(id, request, userId, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{id:guid}/cancel")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CancelOrder(Guid id, [FromBody] string reason, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var result = await _orderService.CancelOrderAsync(id, reason, userId, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService cartService, ILogger<CartController> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<CartDto>), 200)]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var sessionId = GetSessionId();
        var result = await _cartService.GetCartAsync(userId, sessionId, cancellationToken);
        return Ok(result);
    }

    [HttpPost("items")]
    [ProducesResponseType(typeof(ApiResponse<CartDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var sessionId = GetSessionId();
        var result = await _cartService.AddToCartAsync(userId, sessionId, request, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("items/{productId:guid}")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public async Task<IActionResult> UpdateCartItem(Guid productId, [FromBody] UpdateCartItemRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var sessionId = GetSessionId();
        var result = await _cartService.UpdateCartItemAsync(userId, sessionId, productId, request, cancellationToken: cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("items/{productId:guid}")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public async Task<IActionResult> RemoveFromCart(Guid productId, [FromQuery] string? variantId, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var sessionId = GetSessionId();
        var result = await _cartService.RemoveFromCartAsync(userId, sessionId, productId, variantId, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResponse<CartDto>), 200)]
    public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var sessionId = GetSessionId();
        var result = await _cartService.ClearCartAsync(userId, sessionId, cancellationToken);
        return Ok(result);
    }

    private Guid? GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        return claim != null ? Guid.Parse(claim.Value) : null;
    }

    private string? GetSessionId()
    {
        return Request.Headers["X-Session-Id"].FirstOrDefault();
    }
}

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("product/{productId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<List<ReviewDto>>), 200)]
    public async Task<IActionResult> GetProductReviews(Guid productId, CancellationToken cancellationToken)
    {
        var result = await _reviewService.GetProductReviewsAsync(productId, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await _reviewService.CreateReviewAsync(userId, request, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return CreatedAtAction(nameof(GetProductReviews), new { productId = request.ProductId }, result);
    }

    [HttpPost("{reviewId:guid}/vote")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public async Task<IActionResult> VoteReview(Guid reviewId, [FromBody] VoteReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await _reviewService.VoteReviewAsync(reviewId, userId, request.IsHelpful, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{reviewId:guid}/approve")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), 200)]
    public async Task<IActionResult> ApproveReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var result = await _reviewService.ApproveReviewAsync(reviewId, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
