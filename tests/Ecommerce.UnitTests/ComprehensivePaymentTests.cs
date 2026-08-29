using Xunit;
using FluentAssertions;
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

namespace Ecommerce.UnitTests;

public class PaymentDtoComprehensiveTests
{
    [Fact]
    public void PaymentDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PaymentDto
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            PaymentMethod = "CreditCard",
            Amount = 150.00m,
            Currency = "USD",
            Status = "Completed",
            TransactionId = "txn_1234567890",
            CardLast4 = "4242",
            CardType = "Visa",
            ProcessedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.PaymentMethod.Should().Be("CreditCard");
        dto.Amount.Should().Be(150.00m);
        dto.Currency.Should().Be("USD");
        dto.Status.Should().Be("Completed");
        dto.TransactionId.Should().Be("txn_1234567890");
        dto.CardLast4.Should().Be("4242");
    }

    [Fact]
    public void PaymentDto_IsSuccessful_ShouldReturnTrueWhenCompleted()
    {
        var dto = new PaymentDto { Status = "Completed" };

        dto.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PaymentDto_IsSuccessful_ShouldReturnFalseWhenFailed()
    {
        var dto = new PaymentDto { Status = "Failed" };

        dto.IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void PaymentDto_IsSuccessful_ShouldReturnFalseWhenPending()
    {
        var dto = new PaymentDto { Status = "Pending" };

        dto.IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void PaymentDto_CanRefund_ShouldReturnTrueWhenCompleted()
    {
        var dto = new PaymentDto { Status = "Completed" };

        dto.CanRefund.Should().BeTrue();
    }

    [Fact]
    public void PaymentDto_CanRefund_ShouldReturnFalseWhenRefunded()
    {
        var dto = new PaymentDto { Status = "Refunded" };

        dto.CanRefund.Should().BeFalse();
    }

    [Fact]
    public void PaymentDto_CanCapture_ShouldReturnTrueWhenAuthorized()
    {
        var dto = new PaymentDto { Status = "Authorized" };

        dto.CanCapture.Should().BeTrue();
    }

    [Fact]
    public void PaymentDto_CanCapture_ShouldReturnFalseWhenAlreadyCompleted()
    {
        var dto = new PaymentDto { Status = "Completed" };

        dto.CanCapture.Should().BeFalse();
    }

    [Fact]
    public void PaymentDto_MaskedCardNumber_ShouldReturnFormatted()
    {
        var dto = new PaymentDto { CardLast4 = "4242" };

        dto.MaskedCardNumber.Should().Be("**** **** **** 4242");
    }
}

public class PaymentHistoryDtoComprehensiveTests
{
    [Fact]
    public void PaymentHistoryDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PaymentHistoryDto
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            OrderNumber = "ORD-20240101-001",
            PaymentMethod = "CreditCard",
            Amount = 150.00m,
            Currency = "USD",
            Status = "Completed",
            TransactionId = "txn_1234567890",
            ProcessedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.OrderNumber.Should().Be("ORD-20240101-001");
        dto.Amount.Should().Be(150.00m);
    }
}

public class RefundDtoComprehensiveTests
{
    [Fact]
    public void RefundDto_AllProperties_ShouldBeSettable()
    {
        var dto = new RefundDto
        {
            Id = Guid.NewGuid(),
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Amount = 50.00m,
            Currency = "USD",
            Reason = "Product returned",
            Status = "Completed",
            RefundId = "re_1234567890",
            RefundedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Amount.Should().Be(50.00m);
        dto.Reason.Should().Be("Product returned");
        dto.Status.Should().Be("Completed");
        dto.RefundId.Should().Be("re_1234567890");
    }

    [Fact]
    public void RefundDto_IsSuccessful_ShouldReturnTrueWhenCompleted()
    {
        var dto = new RefundDto { Status = "Completed" };

        dto.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void RefundDto_IsSuccessful_ShouldReturnFalseWhenPending()
    {
        var dto = new RefundDto { Status = "Pending" };

        dto.IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void RefundDto_IsSuccessful_ShouldReturnFalseWhenFailed()
    {
        var dto = new RefundDto { Status = "Failed" };

        dto.IsSuccessful.Should().BeFalse();
    }
}

public class ProcessPaymentRequestComprehensiveTests
{
    [Fact]
    public void ProcessPaymentRequest_AllProperties_ShouldBeSettable()
    {
        var request = new ProcessPaymentRequest
        {
            OrderId = Guid.NewGuid(),
            PaymentMethod = "CreditCard",
            Amount = 150.00m,
            Currency = "USD",
            Token = "tok_visa_4242",
            SavePaymentMethod = true,
            BillingAddress = new AddressDto
            {
                Street = "123 Main St",
                City = "New York",
                State = "NY",
                PostalCode = "10001",
                Country = "United States"
            }
        };

        request.OrderId.Should().NotBeEmpty();
        request.PaymentMethod.Should().Be("CreditCard");
        request.Amount.Should().Be(150.00m);
        request.Token.Should().Be("tok_visa_4242");
        request.SavePaymentMethod.Should().BeTrue();
        request.BillingAddress.Should().NotBeNull();
    }
}

public class RefundPaymentRequestComprehensiveTests
{
    [Fact]
    public void RefundPaymentRequest_AllProperties_ShouldBeSettable()
    {
        var request = new RefundPaymentRequest
        {
            PaymentId = Guid.NewGuid(),
            Amount = 50.00m,
            Reason = "Product returned"
        };

        request.PaymentId.Should().NotBeEmpty();
        request.Amount.Should().Be(50.00m);
        request.Reason.Should().Be("Product returned");
    }
}

public class StripePaymentIntentDtoComprehensiveTests
{
    [Fact]
    public void StripePaymentIntentDto_AllProperties_ShouldBeSettable()
    {
        var dto = new StripePaymentIntentDto
        {
            PaymentIntentId = "pi_1234567890",
            ClientSecret = "secret_1234567890",
            Amount = 15000,
            Currency = "usd",
            Status = "requires_payment_method"
        };

        dto.PaymentIntentId.Should().Be("pi_1234567890");
        dto.ClientSecret.Should().Be("secret_1234567890");
        dto.Amount.Should().Be(15000);
        dto.Currency.Should().Be("usd");
        dto.Status.Should().Be("requires_payment_method");
    }

    [Fact]
    public void StripePaymentIntentDto_IsSuccessful_ShouldReturnTrueWhenSucceeded()
    {
        var dto = new StripePaymentIntentDto { Status = "succeeded" };

        dto.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void StripePaymentIntentDto_IsSuccessful_ShouldReturnFalseWhenRequiresAction()
    {
        var dto = new StripePaymentIntentDto { Status = "requires_action" };

        dto.IsSuccessful.Should().BeFalse();
    }
}

public class StripeWebhookDtoComprehensiveTests
{
    [Fact]
    public void StripeWebhookDto_AllProperties_ShouldBeSettable()
    {
        var dto = new StripeWebhookDto
        {
            EventType = "payment_intent.succeeded",
            PaymentIntentId = "pi_1234567890",
            Status = "succeeded",
            Amount = 15000,
            Currency = "usd",
            Metadata = new Dictionary<string, string> { ["OrderId"] = "123" }
        };

        dto.EventType.Should().Be("payment_intent.succeeded");
        dto.PaymentIntentId.Should().Be("pi_1234567890");
        dto.Metadata.Should().ContainKey("OrderId");
    }
}

public class PayPalPaymentDtoComprehensiveTests
{
    [Fact]
    public void PayPalPaymentDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PayPalPaymentDto
        {
            PayPalOrderId = "PAYID-1234567890",
            PayerId = "PAYER-1234567890",
            Amount = 150.00m,
            Currency = "USD",
            Status = "COMPLETED",
            PaymentId = "PAYID-1234567890",
            PayerEmail = "payer@example.com",
            PayerFullName = "John Doe"
        };

        dto.PayPalOrderId.Should().Be("PAYID-1234567890");
        dto.PayerId.Should().Be("PAYER-1234567890");
        dto.Amount.Should().Be(150.00m);
        dto.Status.Should().Be("COMPLETED");
        dto.PayerEmail.Should().Be("payer@example.com");
    }

    [Fact]
    public void PayPalPaymentDto_IsSuccessful_ShouldReturnTrueWhenCompleted()
    {
        var dto = new PayPalPaymentDto { Status = "COMPLETED" };

        dto.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PayPalPaymentDto_IsSuccessful_ShouldReturnFalseWhenPending()
    {
        var dto = new PayPalPaymentDto { Status = "PENDING" };

        dto.IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void PayPalPaymentDto_IsSuccessful_ShouldReturnFalseWhenCancelled()
    {
        var dto = new PayPalPaymentDto { Status = "CANCELLED" };

        dto.IsSuccessful.Should().BeFalse();
    }
}

public class PayPalWebhookDtoComprehensiveTests
{
    [Fact]
    public void PayPalWebhookDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PayPalWebhookDto
        {
            EventType = "PAYMENT.CAPTURE.COMPLETED",
            PayPalOrderId = "PAYID-1234567890",
            Status = "COMPLETED",
            Amount = 150.00m,
            Currency = "USD",
            PayerEmail = "payer@example.com",
            Metadata = new Dictionary<string, string> { ["OrderId"] = "123" }
        };

        dto.EventType.Should().Be("PAYMENT.CAPTURE.COMPLETED");
        dto.PayPalOrderId.Should().Be("PAYID-1234567890");
        dto.Metadata.Should().ContainKey("OrderId");
    }
}
