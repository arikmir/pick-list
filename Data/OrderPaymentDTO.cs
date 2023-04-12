namespace pdfPOC;

public class OrderPaymentDTO
{
     public string Provider { get; set; } = null!;
     public string? CardNumber { get; set; }

     public string Method { get; set; } = null!;
     public PaymentStatus Status { get; set; }
     public OrderPaymentBalancesDTO? Balances { get; set; }
}

public record OrderPaymentBalancesDTO
{
     public decimal OriginalOrderAmount { get; set; }
     public decimal PaidAmount { get; set; }
     public decimal RefundedAmount { get; set; }
     public decimal RefundableBalance()
          => PaidAmount - RefundedAmount;
}