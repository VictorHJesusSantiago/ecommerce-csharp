namespace Ecommerce.Application.Validators.Notification;

public class NotificationDtoValidator : AbstractValidator<NotificationDto>
{
    public NotificationDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Notification title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Notification message is required")
            .MaximumLength(2000).WithMessage("Message cannot exceed 2000 characters");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Notification type is required");
    }
}

public class SendNotificationRequestValidator : AbstractValidator<SendNotificationRequest>
{
    public SendNotificationRequestValidator()
    {
        RuleFor(x => x.RecipientId)
            .NotEmpty().WithMessage("Recipient ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required")
            .MaximumLength(2000).WithMessage("Message cannot exceed 2000 characters");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Notification type is required");
    }
}

public class SendEmailRequestValidator : AbstractValidator<SendEmailRequest>
{
    public SendEmailRequestValidator()
    {
        RuleFor(x => x.To)
            .NotEmpty().WithMessage("Recipient email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required");
    }
}

public class SendBulkEmailRequestValidator : AbstractValidator<SendBulkEmailRequest>
{
    public SendBulkEmailRequestValidator()
    {
        RuleFor(x => x.Recipients)
            .NotEmpty().WithMessage("Recipients list cannot be empty")
            .ForEach(r => r.EmailAddress().WithMessage("Invalid recipient email"));

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required");
    }
}

public class EmailTemplateDtoValidator : AbstractValidator<EmailTemplateDto>
{
    public EmailTemplateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Template name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required");
    }
}

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Link { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public string? Data { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SendNotificationRequest
{
    public Guid RecipientId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Info";
    public string? Link { get; set; }
    public string? Data { get; set; }
    public bool SendEmail { get; set; }
    public bool SendSms { get; set; }
    public bool SendPush { get; set; }
}

public class EmailTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public string? ReplyTo { get; set; }
    public List<string> AvailableVariables { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class SendEmailRequest
{
    public string To { get; set; } = string.Empty;
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = true;
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public List<string> Attachments { get; set; } = [];
    public Dictionary<string, string> TemplateData { get; set; } = [];
}

public class SendBulkEmailRequest
{
    public List<string> Recipients { get; set; } = [];
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = true;
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public string? TemplateName { get; set; }
    public Dictionary<string, string> TemplateData { get; set; } = [];
}

public class SendSmsRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class NotificationPreferenceDto
{
    public Guid UserId { get; set; }
    public bool EmailNotifications { get; set; } = true;
    public bool SmsNotifications { get; set; }
    public bool PushNotifications { get; set; } = true;
    public bool OrderUpdates { get; set; } = true;
    public bool PromotionalEmails { get; set; } = true;
    public bool NewsletterSubscribed { get; set; } = true;
    public bool ReviewNotifications { get; set; } = true;
    public bool PriceDropAlerts { get; set; } = true;
    public bool BackInStockAlerts { get; set; } = true;
    public bool SecurityAlerts { get; set; } = true;
    public bool MarketingEmails { get; set; }
    public bool WeeklyDigest { get; set; } = true;
}
