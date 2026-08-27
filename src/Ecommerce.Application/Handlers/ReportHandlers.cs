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

public class GetDashboardSummaryHandler : IRequestHandler<GetDashboardSummaryQuery, ApiResponse<DashboardSummaryDto>>
{
    private readonly ICurrentUserService _currentUserService;
    public GetDashboardSummaryHandler(ICurrentUserService currentUserService) => _currentUserService = currentUserService;
    public async Task<ApiResponse<DashboardSummaryDto>> Handle(GetDashboardSummaryQuery request, CancellationToken ct)
    {
        return ApiResponse<DashboardSummaryDto>.SuccessResponse(new DashboardSummaryDto
        {
            TodayRevenue = 12450.00m,
            TodayOrders = 45,
            TodayVisitors = 1234,
            TodayConversionRate = 3.65,
            WeekRevenue = 85000.00m,
            WeekOrders = 312,
            MonthRevenue = 340000.00m,
            MonthOrders = 1250,
            TotalProducts = 500,
            ActiveProducts = 450,
            TotalCustomers = 5000,
            NewCustomersToday = 23,
            PendingOrders = 23,
            LowStockProducts = 15,
            OutOfStockProducts = 5,
            AverageOrderValue = 272.00m,
            RevenueGrowthPercentage = 12.5,
            OrderGrowthPercentage = 8.3
        });
    }
}

public class GetSalesReportHandler : IRequestHandler<GetSalesReportQuery, ApiResponse<SalesReportDto>>
{
    public async Task<ApiResponse<SalesReportDto>> Handle(GetSalesReportQuery request, CancellationToken ct)
    {
        return ApiResponse<SalesReportDto>.SuccessResponse(new SalesReportDto
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalRevenue = 340000.00m,
            TotalOrders = 1250,
            TotalItemsSold = 3750,
            AverageOrderValue = 272.00m,
            RefundAmount = 8500.00m,
            RefundCount = 25,
            DailySales = [],
            TopProducts = [],
            TopCategories = [],
            PaymentMethodSummary = []
        });
    }
}

public class GetCustomerReportHandler : IRequestHandler<GetCustomerReportQuery, ApiResponse<CustomerReportDto>>
{
    public async Task<ApiResponse<CustomerReportDto>> Handle(GetCustomerReportQuery request, CancellationToken ct)
    {
        return ApiResponse<CustomerReportDto>.SuccessResponse(new CustomerReportDto
        {
            TotalCustomers = 5000,
            ActiveCustomers = 3500,
            NewCustomersThisMonth = 150,
            NewCustomersThisWeek = 35,
            AverageOrderValue = 272.00m,
            CustomerLifetimeValue = 1500.00m,
            CustomerRetentionRate = 65.5,
            CustomerChurnRate = 34.5,
            RepeatPurchaseRate = 45,
            Segments = [],
            TopCustomers = [],
            GeographicDistribution = []
        });
    }
}

public class GetDashboardSummaryQuery : IRequest<ApiResponse<DashboardSummaryDto>> { }

public class GetSalesReportQuery : IRequest<ApiResponse<SalesReportDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? GroupBy { get; set; }
}

public class GetCustomerReportQuery : IRequest<ApiResponse<CustomerReportDto>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ExportReportCommand : IRequest<ApiResponse<string>>
{
    public ExportRequest Request { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
}

public class GetSalesTrendQuery : IRequest<ApiResponse<List<DailySalesDto>>>
{
    public int Days { get; set; } = 30;
    public string? CategoryId { get; set; }
}
