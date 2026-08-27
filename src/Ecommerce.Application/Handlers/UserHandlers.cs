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

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
{
    private readonly ICurrentUserService _currentUserService;
    public GetUserByIdHandler(ICurrentUserService currentUserService) => _currentUserService = currentUserService;
    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        return ApiResponse<UserDto>.SuccessResponse(new UserDto
        {
            Id = request.Id,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });
    }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ApiResponse<AuthResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    public RegisterUserHandler(ICurrentUserService currentUserService) => _currentUserService = currentUserService;
    public async Task<ApiResponse<AuthResponse>> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        return ApiResponse<AuthResponse>.SuccessResponse(new AuthResponse
        {
            Token = "sample-jwt-token",
            RefreshToken = "sample-refresh-token",
            Expiration = DateTime.UtcNow.AddHours(24),
            User = new UserDto
            {
                Id = Guid.NewGuid(),
                FirstName = request.Request.FirstName,
                LastName = request.Request.LastName,
                Email = request.Request.Email,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        }, "Registration successful");
    }
}

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ApiResponse<AuthResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    public LoginUserHandler(ICurrentUserService currentUserService) => _currentUserService = currentUserService;
    public async Task<ApiResponse<AuthResponse>> Handle(LoginUserCommand request, CancellationToken ct)
    {
        return ApiResponse<AuthResponse>.SuccessResponse(new AuthResponse
        {
            Token = "sample-jwt-token",
            RefreshToken = "sample-refresh-token",
            Expiration = DateTime.UtcNow.AddHours(24),
            User = new UserDto
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = request.Request.Email,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        }, "Login successful");
    }
}

public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, ApiResponse<UserDto>>
{
    private readonly ICurrentUserService _currentUserService;
    public UpdateUserProfileHandler(ICurrentUserService currentUserService) => _currentUserService = currentUserService;
    public async Task<ApiResponse<UserDto>> Handle(UpdateUserProfileCommand request, CancellationToken ct)
    {
        return ApiResponse<UserDto>.SuccessResponse(new UserDto
        {
            Id = request.UserId,
            FirstName = request.Request.FirstName,
            LastName = request.Request.LastName,
            Email = "john@example.com",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        }, "Profile updated successfully");
    }
}

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ApiResponse>
{
    private readonly ICurrentUserService _currentUserService;
    public ChangePasswordHandler(ICurrentUserService currentUserService) => _currentUserService = currentUserService;
    public async Task<ApiResponse> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        return ApiResponse.SuccessResponse("Password changed successfully");
    }
}

public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
{
    public Guid Id { get; set; }
}

public class RegisterUserCommand : IRequest<ApiResponse<AuthResponse>>
{
    public RegisterRequest Request { get; set; } = null!;
}

public class LoginUserCommand : IRequest<ApiResponse<AuthResponse>>
{
    public LoginRequest Request { get; set; } = null!;
}

public class UpdateUserProfileCommand : IRequest<ApiResponse<UserDto>>
{
    public Guid UserId { get; set; }
    public UpdateProfileRequest Request { get; set; } = null!;
}

public class ChangePasswordCommand : IRequest<ApiResponse>
{
    public Guid UserId { get; set; }
    public ChangePasswordRequest Request { get; set; } = null!;
}

public class RefreshTokenCommand : IRequest<ApiResponse<AuthResponse>>
{
    public RefreshTokenRequest Request { get; set; } = null!;
}
