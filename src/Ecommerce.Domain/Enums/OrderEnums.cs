namespace Ecommerce.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Processing = 2,
    Shipped = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Cancelled = 6,
    Returned = 7,
    Refunded = 8,
    PartiallyRefunded = 9,
    OnHold = 10,
    Failed = 11
}

public enum OrderItemStatus
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4,
    Returned = 5,
    Refunded = 6
}

public enum PaymentStatus
{
    Pending = 0,
    Authorized = 1,
    Captured = 2,
    PartiallyCaptured = 3,
    Refunded = 4,
    PartiallyRefunded = 5,
    Voided = 6,
    Failed = 7,
    Expired = 8,
    Disputed = 9,
    Chargeback = 10
}

public enum PaymentMethod
{
    CreditCard = 0,
    DebitCard = 1,
    PayPal = 2,
    BankTransfer = 3,
    CashOnDelivery = 4,
    DigitalWallet = 5,
    GiftCard = 6,
    Cryptocurrency = 7,
    BuyNowPayLater = 8
}

public enum PaymentGateway
{
    Stripe = 0,
    PayPal = 1,
    Square = 2,
    Adyen = 3,
    Braintree = 4,
    AuthorizeNet = 5,
    Worldpay = 6,
    CheckoutCom = 7,
    Mollie = 8,
    Razorpay = 9
}

public enum RefundStatus
{
    Pending = 0,
    Approved = 1,
    Processing = 2,
    Completed = 3,
    Rejected = 4,
    Failed = 5,
    Cancelled = 6
}

public enum RefundReason
{
    CustomerRequest = 0,
    DefectiveProduct = 1,
    WrongItem = 2,
    NotAsDescribed = 3,
    LateDelivery = 4,
    PartialReturn = 5,
    DuplicateCharge = 6,
    Fraudulent = 7,
    Other = 8
}
