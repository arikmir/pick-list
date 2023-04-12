namespace pdfPOC;

public class OrderConsignmentDTO
{
    public string Label { get; set; } = default!;
    public string GivenName { get; set; } = default!;
     public string FamilyName { get; set; } = default!;
     public string Email { get; set; } = default!; 
     public string Phone { get; set; } = default!;
     public string Line1 { get; set; } = default!;
     public string? Line2 { get; set; } = default!;
     public string Locality { get; set; } = default!;
     public string Region { get; set; } = default!;
     public string Postcode { get; set; } = default!;
     public string Country { get; set; } = default!;
}