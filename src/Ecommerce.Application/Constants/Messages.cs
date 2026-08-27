namespace Ecommerce.Application.Constants;

public static class ErrorMessages
{
    public const string EntityNotFound = "The requested {0} was not found.";
    public const string InvalidOperation = "The operation is not valid.";
    public const string Unauthorized = "You are not authorized to perform this action.";
    public const string ValidationFailed = "One or more validation errors occurred.";
    public const string DuplicateEntry = "A record with the same {0} already exists.";
    public const string InsufficientStock = "Insufficient stock available for {0}.";
    public const string PaymentFailed = "Payment processing failed: {0}";
    public const string CouponInvalid = "The coupon code is invalid or has expired.";
    public const string CouponMinNotMet = "Minimum order amount of {0} required for this coupon.";
    public const string CouponUsageExceeded = "This coupon has reached its usage limit.";
    public const string OrderCannotBeCancelled = "This order cannot be cancelled in its current status.";
    public const string OrderCannotBeRefunded = "This order cannot be refunded.";
    public const string RefundExceedsAmount = "Refund amount cannot exceed the order total.";
    public const string ProductNotAvailable = "This product is no longer available.";
    public const string CategoryHasProducts = "Cannot delete category with products. Remove products first.";
    public const string CategoryHasSubcategories = "Cannot delete category with subcategories.";
    public const string EmailAlreadyExists = "An account with this email address already exists.";
    public const string InvalidCredentials = "Invalid email or password.";
    public const string AccountLocked = "Your account has been locked. Please contact support.";
    public const string EmailNotConfirmed = "Please confirm your email address before logging in.";
    public const string PasswordMismatch = "Current password is incorrect.";
    public const string TokenExpired = "The token has expired. Please request a new one.";
    public const string InvalidToken = "Invalid token.";
    public const string RateLimitExceeded = "Too many requests. Please try again later.";
    public const string FileTooLarge = "The file size exceeds the maximum allowed size of {0}MB.";
    public const string InvalidFileType = "The file type is not supported.";
    public const string RequiredField = "The {0} field is required.";
    public const string InvalidFormat = "The {0} format is invalid.";
}

public static class SuccessMessages
{
    public const string OperationCompleted = "Operation completed successfully.";
    public const string ChangesSaved = "Changes saved successfully.";
    public const string RecordCreated = "{0} created successfully.";
    public const string RecordUpdated = "{0} updated successfully.";
    public const string RecordDeleted = "{0} deleted successfully.";
    public const string EmailSent = "Email sent successfully.";
    public const string PasswordReset = "Password reset instructions sent to your email.";
    public const string AccountCreated = "Account created successfully.";
    public const string ProfileUpdated = "Profile updated successfully.";
    public const string OrderPlaced = "Order placed successfully.";
    public const string OrderCancelled = "Order cancelled successfully.";
    public const string PaymentProcessed = "Payment processed successfully.";
    public const string RefundProcessed = "Refund processed successfully.";
    public const string CouponApplied = "Coupon applied successfully.";
    public const string CouponRemoved = "Coupon removed.";
    public const string ReviewSubmitted = "Review submitted for approval.";
    public const string NewsletterSubscribed = "Successfully subscribed to newsletter.";
    public const string NewsletterUnsubscribed = "Successfully unsubscribed from newsletter.";
    public const string ItemAddedToCart = "Item added to cart.";
    public const string ItemRemovedFromCart = "Item removed from cart.";
    public const string ItemAddedToWishlist = "Item added to wishlist.";
    public const string ItemRemovedFromWishlist = "Item removed from wishlist.";
}

public static class LogMessages
{
    public const string EntityCreated = "{EntityName} {EntityId} created by {UserId}";
    public const string EntityUpdated = "{EntityName} {EntityId} updated by {UserId}";
    public const string EntityDeleted = "{EntityName} {EntityId} deleted by {UserId}";
    public const string UserLogin = "User {UserId} logged in from {IpAddress}";
    public const string UserLogout = "User {UserId} logged out";
    public const string PasswordChanged = "Password changed for user {UserId}";
    public const string PaymentProcessed = "Payment {PaymentId} processed for order {OrderId}";
    public const string PaymentFailed = "Payment failed for order {OrderId}: {Reason}";
    public const string RefundProcessed = "Refund {RefundId} processed for order {OrderId}";
    public const string OrderStatusChanged = "Order {OrderNumber} status changed from {OldStatus} to {NewStatus}";
    public const string StockAdjusted = "Stock adjusted for product {ProductId} in warehouse {WarehouseId}";
    public const string EmailSent = "Email sent to {Recipient}: {Subject}";
    public const string CacheHit = "Cache hit for key {Key}";
    public const string CacheMiss = "Cache miss for key {Key}";
    public const string ExternalServiceCall = "Calling external service {ServiceName}: {Url}";
    public const string ExternalServiceResponse = "Response from {ServiceName}: {StatusCode} in {ElapsedMs}ms";
}
