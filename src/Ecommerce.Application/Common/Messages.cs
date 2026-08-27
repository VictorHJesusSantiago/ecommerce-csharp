namespace Ecommerce.Application.Common;

public static class ErrorMessages
{
    public const string ProductNotFound = "Product not found.";
    public const string CategoryNotFound = "Category not found.";
    public const string OrderNotFound = "Order not found.";
    public const string UserNotFound = "User not found.";
    public const string CartNotFound = "Cart not found.";
    public const string ReviewNotFound = "Review not found.";
    public const string CouponNotFound = "Coupon not found or expired.";
    public const string PaymentFailed = "Payment processing failed. Please try again.";
    public const string InsufficientStock = "Insufficient stock available.";
    public const string DuplicateEmail = "A user with this email already exists.";
    public const string InvalidCredentials = "Invalid email or password.";
    public const string AccountLocked = "Account has been locked. Please contact support.";
    public const string EmailNotConfirmed = "Please confirm your email address.";
    public const string TokenExpired = "Your session has expired. Please log in again.";
    public const string InvalidToken = "Invalid authentication token.";
    public const string CannotCancelOrder = "Cannot cancel order in current status.";
    public const string AlreadyReviewed = "You have already reviewed this product.";
    public const string ReviewNotApproved = "This review is pending approval.";
    public const string FileTooLarge = "File size exceeds the maximum allowed size.";
    public const string InvalidFileType = "File type is not allowed.";
    public const string WishlistFull = "Your wishlist is full. Remove items to add new ones.";
    public const string CartLimitReached = "You have reached the maximum cart limit.";
    public const string CouponAlreadyUsed = "This coupon has already been used.";
    public const string CouponNotValid = "This coupon is not valid for this order.";
    public const string AddressNotFound = "Address not found.";
    public const string PaymentMethodNotFound = "Payment method not found.";
    public const string RefundNotAllowed = "Refund is not allowed for this order.";
    public const string ShippingNotAvailable = "Shipping is not available to this address.";
    public const string InventoryTransferFailed = "Inventory transfer failed.";
    public const string WarehouseNotFound = "Warehouse not found.";
    public const string SupplierNotFound = "Supplier not found.";
}

public static class SuccessMessages
{
    public const string ProductCreated = "Product created successfully.";
    public const string ProductUpdated = "Product updated successfully.";
    public const string ProductDeleted = "Product deleted successfully.";
    public const string CategoryCreated = "Category created successfully.";
    public const string CategoryUpdated = "Category updated successfully.";
    public const string CategoryDeleted = "Category deleted successfully.";
    public const string OrderPlaced = "Order placed successfully.";
    public const string OrderCancelled = "Order cancelled successfully.";
    public const string OrderStatusUpdated = "Order status updated successfully.";
    public const string PaymentProcessed = "Payment processed successfully.";
    public const string RefundProcessed = "Refund processed successfully.";
    public const string ReviewSubmitted = "Review submitted successfully. It will be visible after approval.";
    public const string ReviewApproved = "Review approved successfully.";
    public const string ReviewDeleted = "Review deleted successfully.";
    public const string CouponApplied = "Coupon applied successfully.";
    public const string CouponRemoved = "Coupon removed successfully.";
    public const string ProfileUpdated = "Profile updated successfully.";
    public const string PasswordChanged = "Password changed successfully.";
    public const string AddressCreated = "Address created successfully.";
    public const string AddressUpdated = "Address updated successfully.";
    public const string AddressDeleted = "Address deleted successfully.";
    public const string WishlistItemAdded = "Item added to wishlist.";
    public const string WishlistItemRemoved = "Item removed from wishlist.";
    public const string CartItemAdded = "Item added to cart.";
    public const string CartItemUpdated = "Cart item updated.";
    public const string CartItemRemoved = "Item removed from cart.";
    public const string CartCleared = "Cart cleared successfully.";
    public const string EmailSent = "Email sent successfully.";
    public const string SmsSent = "SMS sent successfully.";
    public const string NewsletterSubscribed = "Successfully subscribed to newsletter.";
    public const string NewsletterUnsubscribed = "Successfully unsubscribed from newsletter.";
    public const string StockAdjusted = "Stock adjusted successfully.";
    public const string WarehouseCreated = "Warehouse created successfully.";
    public const string SupplierCreated = "Supplier created successfully.";
}

public static class LogMessages
{
    public const string EntityCreated = "Entity {EntityType} created with ID {EntityId}.";
    public const string EntityUpdated = "Entity {EntityType} with ID {EntityId} updated.";
    public const string EntityDeleted = "Entity {EntityType} with ID {EntityId} deleted.";
    public const string UserLoggedIn = "User {UserId} logged in from {IpAddress}.";
    public const string UserLoggedOut = "User {UserId} logged out.";
    public const string OrderPlaced = "Order {OrderNumber} placed by user {UserId}.";
    public const string OrderCancelled = "Order {OrderNumber} cancelled. Reason: {Reason}.";
    public const string PaymentProcessed = "Payment {PaymentId} processed for order {OrderNumber}.";
    public const string PaymentFailed = "Payment failed for order {OrderNumber}. Error: {Error}.";
    public const string CacheHit = "Cache hit for key: {CacheKey}.";
    public const string CacheMiss = "Cache miss for key: {CacheKey}.";
    public const string ExternalApiCall = "External API call: {Method} {Url} responded with {StatusCode} in {ElapsedMs}ms.";
    public const string BackgroundJobStarted = "Background job {JobName} started.";
    public const string BackgroundJobCompleted = "Background job {JobName} completed in {ElapsedMs}ms.";
    public const string EmailSent = "Email sent to {To}: {Subject}.";
    public const string SmsSent = "SMS sent to {PhoneNumber}.";
    public const string RateLimitExceeded = "Rate limit exceeded for IP {IpAddress}.";
    public const string SecurityEvent = "Security event: {EventType} for user {UserId}.";
}
