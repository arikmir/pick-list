using System;
using System.Linq;
using QuestPDF.Helpers;

namespace pdfPOC;

public class FakeDataSource
{
    private static Random Random = new Random();

    public static printOrderDTO GetInvoiceDetails()
    {
        var items = Enumerable
            .Range(1, 8)
            .Select(i => GenerateRandomOrderItem())
            .ToList();

        return new printOrderDTO()
        {
            OrderNumber = Random.Next(1_000, 10_000),
            CreatedAt = DateTime.Now,
            BillingConsignment = GenerateRandomAddress(),
            Items = items,
            Total = (decimal) Math.Round(Random.NextDouble() * 100, 2)
        };
    }

    private static OrderItemDTO GenerateRandomOrderItem()
    {
        return new OrderItemDTO
        {
            Name = Placeholders.Label(),
            PriceExcludingTax = (decimal) Math.Round(Random.NextDouble() * 100, 2),
            PriceIncludingTax = (decimal) Math.Round(Random.NextDouble() * 100, 2),
            Quantity = Random.Next(1, 10)
        };
    }

    private static OrderConsignmentDTO GenerateRandomAddress()
    {
        return new OrderConsignmentDTO()
        {
            GivenName = Placeholders.Name(),
            FamilyName = Placeholders.Name(),
            Line1 = Placeholders.Label(),
            Line2 = Placeholders.Label(),
            Locality = Placeholders.Label(),
            Postcode = Placeholders.Label(),
            Country = Placeholders.Label(),
            Region = Placeholders.Label(),
            Email = Placeholders.Email(),
            Phone = Placeholders.PhoneNumber()
        };
    }
}