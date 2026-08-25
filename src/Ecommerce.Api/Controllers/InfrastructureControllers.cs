using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    public IActionResult GetHealth()
    {
        var healthData = new
        {
            Status = "Healthy",
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development",
            Timestamp = DateTime.UtcNow,
            Uptime = Process.GetCurrentProcess().StartTime.ToUniversalTime()
        };
        return Ok(ApiResponse<object>.SuccessResponse(healthData));
    }

    [HttpGet("ready")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    public IActionResult GetReadiness()
    {
        return Ok(ApiResponse<object>.SuccessResponse(new { Ready = true }));
    }
}

[ApiController]
[Route("api/v1/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger) => _logger = logger;

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    public IActionResult GetDashboard()
    {
        var dashboard = new
        {
            TodayRevenue = 12450.50m,
            TodayOrders = 45,
            ThisMonthRevenue = 285000.00m,
            ThisMonthOrders = 1250,
            RevenueGrowth = 12.5,
            OrderGrowth = 8.3,
            TotalProducts = 1520,
            TotalCustomers = 8750,
            PendingOrders = 23,
            LowStockProducts = 15
        };
        return Ok(ApiResponse<object>.SuccessResponse(dashboard));
    }

    [HttpGet("sales")]
    [Authorize(Roles = "Admin,Manager")]
    public IActionResult GetSalesReport([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        return Ok(ApiResponse.SuccessResponse("Sales report generated."));
    }
}

[ApiController]
[Route("api/v1/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger) => _logger = logger;

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public IActionResult GetInventory()
    {
        return Ok(ApiResponse.SuccessResponse("Inventory data."));
    }

    [HttpGet("low-stock")]
    [Authorize(Roles = "Admin,Manager")]
    public IActionResult GetLowStock()
    {
        return Ok(ApiResponse.SuccessResponse("Low stock items."));
    }

    [HttpPost("adjust")]
    [Authorize(Roles = "Admin,Manager")]
    public IActionResult AdjustStock([FromBody] object request)
    {
        return Ok(ApiResponse.SuccessResponse("Stock adjusted successfully."));
    }
}
