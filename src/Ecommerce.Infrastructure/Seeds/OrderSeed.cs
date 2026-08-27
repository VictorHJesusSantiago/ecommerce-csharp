using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.User;

namespace Ecommerce.Infrastructure.Seeds;

public static class OrderSeed
{
    public static List<Order> GetOrders(List<ApplicationUser> users)
    {
        if (users.Count == 0) return [];

        var orders = new List<Order>();
        var random = new Random(42);
        var statuses = new[] { OrderStatus.Pending, OrderStatus.Processing, OrderStatus.Shipped, OrderStatus.Delivered, OrderStatus.Cancelled };

        for (int i = 0; i < 50; i++)
        {
            var subtotal = Math.Round((decimal)(random.NextDouble() * 500 + 10), 2);
            var tax = Math.Round(subtotal * 0.08m, 2);
            var shipping = random.Next(3) == 0 ? 0 : 9.99m;
            var discount = random.Next(5) == 0 ? Math.Round(subtotal * 0.1m, 2) : 0;

            orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = $"ORD-{DateTime.UtcNow.AddDays(-random.Next(90)).ToString("yyyyMMdd")}-{i + 1:D4}",
                UserId = users[random.Next(users.Count)].Id,
                SubTotal = subtotal,
                TaxAmount = tax,
                ShippingCost = shipping,
                DiscountAmount = discount,
                TotalAmount = subtotal + tax + shipping - discount,
                Status = statuses[random.Next(statuses.Length)],
                PaymentStatus = random.Next(3) == 0 ? PaymentStatus.Pending : PaymentStatus.Paid,
                CreatedAt = DateTime.UtcNow.AddDays(-random.Next(90))
            });
        }

        return orders;
    }
}
