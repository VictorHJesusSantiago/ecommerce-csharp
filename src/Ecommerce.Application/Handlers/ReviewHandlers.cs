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

public class GetReviewsByProductHandler : IRequestHandler<GetReviewsByProductQuery, ApiResponse<PagedResponse<ReviewDto>>>
{
    private readonly IReviewService _reviewService;
    public GetReviewsByProductHandler(IReviewService reviewService) => _reviewService = reviewService;
    public async Task<ApiResponse<PagedResponse<ReviewDto>>> Handle(GetReviewsByProductQuery request, CancellationToken ct)
    {
        return await _reviewService.GetReviewsByProductIdAsync(request.ProductId, request.SearchRequest, ct);
    }
}

public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, ApiResponse<ReviewDto>>
{
    private readonly IReviewService _reviewService;
    public CreateReviewHandler(IReviewService reviewService) => _reviewService = reviewService;
    public async Task<ApiResponse<ReviewDto>> Handle(CreateReviewCommand request, CancellationToken ct)
    {
        return await _reviewService.CreateReviewAsync(request.UserId, request.Request, ct);
    }
}

public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, ApiResponse<ReviewDto>>
{
    private readonly IReviewService _reviewService;
    public UpdateReviewHandler(IReviewService reviewService) => _reviewService = reviewService;
    public async Task<ApiResponse<ReviewDto>> Handle(UpdateReviewCommand request, CancellationToken ct)
    {
        return await _reviewService.UpdateReviewAsync(request.ReviewId, request.UserId, request.Request, ct);
    }
}

public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, ApiResponse>
{
    private readonly IReviewService _reviewService;
    public DeleteReviewHandler(IReviewService reviewService) => _reviewService = reviewService;
    public async Task<ApiResponse> Handle(DeleteReviewCommand request, CancellationToken ct)
    {
        return await _reviewService.DeleteReviewAsync(request.ReviewId, request.UserId, ct);
    }
}

public class VoteReviewHandler : IRequestHandler<VoteReviewCommand, ApiResponse>
{
    private readonly IReviewService _reviewService;
    public VoteReviewHandler(IReviewService reviewService) => _reviewService = reviewService;
    public async Task<ApiResponse> Handle(VoteReviewCommand request, CancellationToken ct)
    {
        return await _reviewService.VoteReviewAsync(request.ReviewId, request.UserId, request.Request, ct);
    }
}

public class GetReviewStatsHandler : IRequestHandler<GetReviewStatsQuery, ApiResponse<ReviewStatsDto>>
{
    private readonly IReviewService _reviewService;
    public GetReviewStatsHandler(IReviewService reviewService) => _reviewService = reviewService;
    public async Task<ApiResponse<ReviewStatsDto>> Handle(GetReviewStatsQuery request, CancellationToken ct)
    {
        return await _reviewService.GetReviewStatsAsync(request.ProductId, request.UserId, ct);
    }
}

public class GetReviewsByProductQuery : IRequest<ApiResponse<PagedResponse<ReviewDto>>>
{
    public Guid ProductId { get; set; }
    public ReviewSearchRequest SearchRequest { get; set; } = null!;
}

public class CreateReviewCommand : IRequest<ApiResponse<ReviewDto>>
{
    public Guid UserId { get; set; }
    public CreateReviewRequest Request { get; set; } = null!;
}

public class UpdateReviewCommand : IRequest<ApiResponse<ReviewDto>>
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }
    public UpdateReviewRequest Request { get; set; } = null!;
}

public class DeleteReviewCommand : IRequest<ApiResponse>
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }
}

public class VoteReviewCommand : IRequest<ApiResponse>
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }
    public VoteReviewRequest Request { get; set; } = null!;
}

public class GetReviewStatsQuery : IRequest<ApiResponse<ReviewStatsDto>>
{
    public Guid ProductId { get; set; }
    public Guid? UserId { get; set; }
}
