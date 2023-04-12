using System;

namespace pdfPOC;

public class OrderHeaderDTO
{
    public string OrderId { get; set; } = default!;
     public string? OrderNumber { get; set; }
     public string CustomerGivenName { get; set; } = default!;
     public string CustomerFamilyName { get; set; } = default!;
     public string BillingPhone { get; set; } = default!;
     public string BillingEmail { get; set; } = default!;
     public string State { get; set; }  = default!;
     public DeliveryMethod DeliveryMethod { get; set; }
     public DeliveryStatus DeliveryStatus { get; set; }
     public PaymentStatus PaymentStatus { get; set; }
     public decimal Total { get; set; }
     public OrderHeaderFlagsDTO Flags { get; set; } = new();
     public DateTimeOffset CreatedAt { get; set; }
     public DateTimeOffset UpdatedAt { get; set; }
     public string? CustomerIdentifier { get; set; }
}

public enum PaymentStatus
{
    Unset,
    Unpaid,
    Validated,
    Failed,
    Paid,
    PartiallyRefunded,
    FullyRefunded,
    TakenManually
}

public enum DeliveryStatus
{
    Unset,

    NotDispatched,
    InTransit,
    Delivered,

    ReadyToCollect,
    Collected
}

public enum DeliveryMethod
{
    Unset,
    Pickup,
    Delivery
}