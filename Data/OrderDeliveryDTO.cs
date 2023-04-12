namespace pdfPOC;

public class OrderDeliveryDTO
{
     public string Provider { get; set; } = default!;
     public string Method { get; set; } = default!;
     public string Status { get; set; } = default!;
     public decimal PriceExcludingTax { get; set; }
     public decimal PriceIncludingTax { get; set; }
     public string? ProviderReference { get; set; }
}