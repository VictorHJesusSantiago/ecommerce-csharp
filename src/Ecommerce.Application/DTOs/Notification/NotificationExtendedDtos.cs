namespace Ecommerce.Application.DTOs.Notification;

public class NotificationExtendedDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? UserEmail { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? Link { get; set; }
    public string? ImageUrl { get; set; }
    public string? ActionText { get; set; }
    public string? ActionUrl { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? ArchivedAt { get; set; }
    public string? Data { get; set; }
    public string Priority { get; set; } = "Normal";
    public bool SendEmail { get; set; }
    public bool SendSms { get; set; }
    public bool SendPush { get; set; }
    public bool EmailSent { get; set; }
    public bool SmsSent { get; set; }
    public bool PushSent { get; set; }
    public DateTime? EmailSentAt { get; set; }
    public DateTime? SmsSentAt { get; set; }
    public DateTime? PushSentAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

public class NotificationBatchDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Category { get; set; }
    public int TotalRecipients { get; set; }
    public int SentCount { get; set; }
    public int DeliveredCount { get; set; }
    public int OpenedCount { get; set; }
    public int ClickedCount { get; set; }
    public int FailedCount { get; set; }
    public decimal DeliveryRate { get; set; }
    public decimal OpenRate { get; set; }
    public decimal ClickRate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class NotificationAnalyticsDto
{
    public int TotalNotifications { get; set; }
    public int SentNotifications { get; set; }
    public int DeliveredNotifications { get; set; }
    public int OpenedNotifications { get; set; }
    public int ClickedNotifications { get; set; }
    public int FailedNotifications { get; set; }
    public decimal DeliveryRate { get; set; }
    public decimal OpenRate { get; set; }
    public decimal ClickRate { get; set; }
    public decimal FailureRate { get; set; }
    public List<NotificationTypeBreakdownDto> TypeBreakdown { get; set; } = [];
    public List<DailyNotificationDto> DailyNotifications { get; set; } = [];
    public List<NotificationChannelPerformanceDto> ChannelPerformance { get; set; } = [];
}

public class NotificationTypeBreakdownDto
{
    public string Type { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
    public decimal DeliveryRate { get; set; }
    public decimal OpenRate { get; set; }
    public decimal ClickRate { get; set; }
}

public class DailyNotificationDto
{
    public DateTime Date { get; set; }
    public int SentCount { get; set; }
    public int DeliveredCount { get; set; }
    public int OpenedCount { get; set; }
    public int ClickedCount { get; set; }
    public int FailedCount { get; set; }
}

public class NotificationChannelPerformanceDto
{
    public string Channel { get; set; } = string.Empty;
    public int TotalSent { get; set; }
    public int Delivered { get; set; }
    public int Opened { get; set; }
    public int Clicked { get; set; }
    public int Failed { get; set; }
    public decimal DeliveryRate { get; set; }
    public decimal OpenRate { get; set; }
    public decimal ClickRate { get; set; }
    public decimal AverageDeliveryTime { get; set; }
}

public class NotificationPreferenceExtendedDto
{
    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public EmailNotificationSettingsDto Email { get; set; } = new();
    public SmsNotificationSettingsDto Sms { get; set; } = new();
    public PushNotificationSettingsDto Push { get; set; } = new();
    public InAppNotificationSettingsDto InApp { get; set; } = new();
    public string QuietHoursStart { get; set; } = "22:00";
    public string QuietHoursEnd { get; set; } = "08:00";
    public string Timezone { get; set; } = "UTC";
    public DateTime? LastUpdated { get; set; }
}

public class EmailNotificationSettingsDto
{
    public bool OrderUpdates { get; set; } = true;
    public bool ShippingUpdates { get; set; } = true;
    public bool DeliveryUpdates { get; set; } = true;
    public bool PaymentUpdates { get; set; } = true;
    public bool Promotions { get; set; }
    public bool Newsletter { get; set; } = true;
    public bool ProductRecommendations { get; set; }
    public bool PriceDropAlerts { get; set; } = true;
    public bool BackInStockAlerts { get; set; } = true;
    public bool ReviewRequests { get; set; } = true;
    public bool SecurityAlerts { get; set; } = true;
    public bool AccountUpdates { get; set; } = true;
    public bool WeeklyDigest { get; set; } = true;
    public bool MonthlyReport { get; set; }
}

public class SmsNotificationSettingsDto
{
    public bool OrderUpdates { get; set; }
    public bool ShippingUpdates { get; set; } = true;
    public bool DeliveryUpdates { get; set; } = true;
    public bool PaymentUpdates { get; set; }
    public bool Promotions { get; set; }
    public bool SecurityAlerts { get; set; } = true;
    public bool TwoFactorCode { get; set; } = true;
}

public class PushNotificationSettingsDto
{
    public bool OrderUpdates { get; set; } = true;
    public bool ShippingUpdates { get; set; } = true;
    public bool DeliveryUpdates { get; set; } = true;
    public bool PaymentUpdates { get; set; }
    public bool Promotions { get; set; }
    public bool PriceDropAlerts { get; set; } = true;
    public bool BackInStockAlerts { get; set; } = true;
    public bool NewMessages { get; set; } = true;
    public bool Recommendations { get; set; }
}

public class InAppNotificationSettingsDto
{
    public bool OrderUpdates { get; set; } = true;
    public bool ShippingUpdates { get; set; } = true;
    public bool PaymentUpdates { get; set; } = true;
    public bool Promotions { get; set; } = true;
    public bool SystemAlerts { get; set; } = true;
    public bool Mentions { get; set; } = true;
    public bool Comments { get; set; } = true;
    public bool Likes { get; set; } = true;
}

public class UpdateNotificationPreferenceExtendedRequest
{
    public EmailNotificationSettingsDto? Email { get; set; }
    public SmsNotificationSettingsDto? Sms { get; set; }
    public PushNotificationSettingsDto? Push { get; set; }
    public InAppNotificationSettingsDto? InApp { get; set; }
    public string? QuietHoursStart { get; set; }
    public string? QuietHoursEnd { get; set; }
    public string? Timezone { get; set; }
}

public class NotificationTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string? HtmlBody { get; set; }
    public string? SmsBody { get; set; }
    public string? PushTitle { get; set; }
    public string? PushBody { get; set; }
    public bool IsActive { get; set; } = true;
    public List<string> AvailableVariables { get; set; } = [];
    public string? Description { get; set; }
    public int UsageCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class SendBulkNotificationRequest
{
    public string TemplateName { get; set; } = string.Empty;
    public List<string> RecipientEmails { get; set; } = [];
    public List<Guid> RecipientUserIds { get; set; } = [];
    public string? SegmentName { get; set; }
    public Dictionary<string, string> Variables { get; set; } = [];
    public bool SendEmail { get; set; } = true;
    public bool SendSms { get; set; }
    public bool SendPush { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public string? CampaignName { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public bool TrackOpens { get; set; } = true;
    public bool TrackClicks { get; set; } = true;
}

public class SendBulkNotificationResult
{
    public Guid BatchId { get; set; }
    public string BatchName { get; set; } = string.Empty;
    public int TotalRecipients { get; set; }
    public int QueuedCount { get; set; }
    public int FailedCount { get; set; }
    public List<string> Errors { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public string? Status { get; set; }
}
