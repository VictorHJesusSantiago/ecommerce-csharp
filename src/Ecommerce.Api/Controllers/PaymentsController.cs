using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    [HttpPost("process")]
    public IActionResult ProcessPayment([FromBody] object request)
    {
        return Ok(new { transactionId = Guid.NewGuid(), status = "processed" });
    }

    [HttpPost("refund/{transactionId:guid}")]
    public IActionResult Refund(Guid transactionId, [FromBody] object request)
    {
        return Ok(new { refundId = Guid.NewGuid(), transactionId });
    }

    [HttpGet("transactions/{id:guid}")]
    public IActionResult GetTransaction(Guid id)
    {
        return Ok(new { id });
    }

    [HttpPost("webhooks/stripe")]
    public IActionResult StripeWebhook()
    {
        return Ok();
    }

    [HttpPost("webhooks/paypal")]
    public IActionResult PayPalWebhook()
    {
        return Ok();
    }
}
