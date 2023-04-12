using System.Collections.Generic;

namespace pdfPOC;

public class OrderItemDTO
{
     public string ItemId { get; set; } = default!;
     public string Kind { get; set; } = default!;
    public string State { get; set; } = null!;
     public string Name { get; set; } = null!;
    public decimal? PriceExcludingTax { get; set; }
     public decimal? PriceIncludingTax { get; set; }
     public int Quantity { get; set; }
    public Dictionary<string,string> Extensions { get; set; } = new();
    public decimal? OriginalPriceExcludingTax { get; set; }
    public decimal? OriginalPriceIncludingTax { get; set; }
     public int OriginalQuantity { get; set; }
    public bool CanEnqueue { get; set; }
    public string? CanEnqueueTo { get; set; }
    public string? CanEnqueueToId { get; set; }
     public bool CanSubstitute { get; set; }
}