namespace pdfPOC;

public class OrderHeaderFlagsDTO
{
    public bool HasFailedDelivery { get; set; }
    public bool HasMessages { get; set; }
    public bool HasNewMessage { get; set; }
    public bool HasNotes { get; set; }
    public bool HasPoItems { get; set; }
    public bool HasPrescriptionItems { get; set; }
    public bool HasS3Items { get; set; }
    public bool HasPhotoScriptItems { get; set; }
    public bool IsLoyaltyCustomer { get; set; }
    public bool CustomerRequestsRewardsLookup { get; set; }
    public bool CustomerRequestsCallback { get; set; }
}