using Ecommerce.Domain.Entities.Marketing;

namespace Ecommerce.Infrastructure.Seeds;

public static class NewsletterSeed
{
    public static List<NewsletterSubscriber> GetSubscribers()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Email = "subscriber1@example.com",
                IsActive = true,
                SubscribedAt = DateTime.UtcNow.AddDays(-60)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "subscriber2@example.com",
                IsActive = true,
                SubscribedAt = DateTime.UtcNow.AddDays(-45)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "subscriber3@example.com",
                IsActive = true,
                SubscribedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "subscriber4@example.com",
                IsActive = false,
                SubscribedAt = DateTime.UtcNow.AddDays(-20),
                UnsubscribedAt = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "subscriber5@example.com",
                IsActive = true,
                SubscribedAt = DateTime.UtcNow.AddDays(-15)
            }
        ];
    }
}
