using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace pdfPOC
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var order = FakeDataSource.GetInvoiceDetails();
            var document = new PickListDocument(order);
            document.GeneratePdf("/Users/arikmir/Desktop/test.pdf");

        }
    }
}