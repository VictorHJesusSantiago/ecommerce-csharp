namespace Ecommerce.Application.DTOs.Notification;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "Email", "Sms", "Push", "InApp"
    public string? ActionUrl { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}

public class SendNotificationRequest
{
    public Guid? UserId { get; set; }
    public List<Guid>? UserIds { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "InApp";
    public string? ActionUrl { get; set; }
    public string? TemplateId { get; set; }
    public Dictionary<string, string>? TemplateData { get; set; }
}

public class EmailTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? Category { get; set; }
    public Dictionary<string, string> Variables { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class NewsletterSubscriberDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime SubscribedAt { get; set; }
    public DateTime? UnsubscribedAt { get; set; }
}

public class SendEmailRequest
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = true;
    public string? FromEmail { get; set; }
    public string? FromName { get; set; }
    public List<string>? Cc { get; set; }
    public List<string>? Bcc { get; set; }
    public List<EmailAttachmentDto>? Attachments { get; set; }
}

public class EmailAttachmentDto
{
    public string FileName { get; set; } = string.Empty;
    public byte[] Content { get; set; } = [];
    public string ContentType { get; set; } = "application/octet-stream";
}

public class SendBulkEmailRequest
{
    public List<string> Recipients { get; set; } = [];
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = true;
}

public class SendSmsRequest
{
    public string To { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class NotificationPreferenceDto
{
    public Guid UserId { get; set; }
    public bool EmailEnabled { get; set; } = true;
    public bool SmsEnabled { get; set; }
    public bool PushEnabled { get; set; } = true;
    public bool OrderUpdates { get; set; } = true;
    public bool Promotions { get; set; } = true;
    public bool Newsletter { get; set; } = true;
    public bool ProductAlerts { get; set; }
}
