using System;
using System.Collections.Generic;

namespace pdfPOC
{
    public class printOrderDTO
    {
        public string OrderId { get; set; } = default!;
        public string StoreId { get; set; } = default!;
        public string? CustomerIdentifier { get; set; }
        public bool Enrolled { get; set; }
        public string? ExternalIdentifier { get; set; }
        public OrderHeaderDTO Header { get; set; } = default!;
        public int OrderNumber { get; set; }
        public string State { get; set; } = null!;
        public decimal Total { get; set; }
        public OrderCollectionMethodKind DeliveryMethod { get; set; }
        public OrderConsignmentDTO? BillingConsignment { get; set; }
        public OrderConsignmentDTO? DeliveryConsignment { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();

        public string? Voucher { get; set; }
        public List<OrderNoteDTO> Notes { get; set; } = new();
        public OrderDeliveryDTO? Delivery { get; set; }
         public OrderPaymentDTO? Payment { get; set; }
        public string? CustomerNotes { get; set; }
        public string? CustomerRewardsNumber { get; set; } ="testRewardsNumber";
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}