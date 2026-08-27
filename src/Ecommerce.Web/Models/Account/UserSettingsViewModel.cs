namespace Ecommerce.Web.Models.Account;

public class ChangePasswordViewModel
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;
}

public class NotificationPreferencesViewModel
{
    public bool OrderUpdates { get; set; } = true;
    public bool Promotions { get; set; } = true;
    public bool Newsletter { get; set; } = true;
    public bool ProductAlerts { get; set; } = true;
    public bool ReviewReminders { get; set; } = true;
    public bool SecurityAlerts { get; set; } = true;
    public bool EmailEnabled { get; set; } = true;
    public bool SmsEnabled { get; set; } = false;
    public bool PushEnabled { get; set; } = false;
}

public class ActivityLogViewModel
{
    public List<ActivityLogEntry> Activities { get; set; } = new();
}

public class ActivityLogEntry
{
    public string Action { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string? IpAddress { get; set; }
    public string? Device { get; set; }
}

public class TwoFactorAuthViewModel
{
    public bool IsEnabled { get; set; }
    public string? QrCodeUrl { get; set; }
    public string? RecoveryCode { get; set; }
    public string VerificationCode { get; set; } = string.Empty;
}
