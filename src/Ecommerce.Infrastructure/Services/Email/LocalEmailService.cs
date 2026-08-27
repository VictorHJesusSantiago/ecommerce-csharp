using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalEmailService : IEmailService
{
    private readonly ILogger<LocalEmailService> _logger;
    private readonly SmtpSettings _settings;

    public LocalEmailService(ILogger<LocalEmailService> logger, SmtpSettings settings)
    {
        _logger = logger;
        _settings = settings;
    }

    public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        _logger.LogInformation("Email sent to {To} with subject: {Subject}", to, subject);
        return Task.CompletedTask;
    }

    public Task SendEmailWithAttachmentAsync(string to, string subject, string body, Stream attachment, string fileName, bool isHtml = true)
    {
        _logger.LogInformation("Email with attachment sent to {To} with subject: {Subject}, file: {File}", to, subject, fileName);
        return Task.CompletedTask;
    }

    public Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true)
    {
        _logger.LogInformation("Bulk email sent to {Count} recipients with subject: {Subject}", recipients.Count, subject);
        return Task.CompletedTask;
    }

    public Task SendTemplateEmailAsync(string to, string templateName, Dictionary<string, string> parameters)
    {
        _logger.LogInformation("Template email sent to {To} with template: {Template}", to, templateName);
        return Task.CompletedTask;
    }
}
