using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using iTextSharp.text;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using Document = QuestPDF.Fluent.Document;

namespace pdfPOC;

public class PickListDocument : IDocument
{

    private readonly printOrderDTO order;
    public PickListDocument(printOrderDTO printOrderDTO)
    {
        order = printOrderDTO;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(50);
            page.PageColor(Colors.White);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }
    public void ComposeHeader(IContainer container)
    {
        

        var titleStyle = TextStyle.Default
            .ExtraBold()
            .FontColor(Colors.Black)
            .FontFamily(Fonts.Arial);
        var textStyle = TextStyle.Default
            .Light()
            .FontColor(Colors.Black)
            .FontSize(14)
            .FontFamily(Fonts.Arial);
        container.Row(row =>
        {
            row.RelativeItem(2).Column(column =>
            {
                if (order.Delivery is { Method: "Delivery" })
                {
                    column.Item()
                        .Text("DELIVERY")
                        .FontSize(16)
                        .Style(titleStyle);
                }
                column.Item()
                    .Text("CLICK & COLLECT")
                    .FontSize(16)
                    .Style(titleStyle);

                column.Item().Text(text =>
                {
                    text.Span($"Order Pick List for #{order.OrderNumber}")
                        .Style(titleStyle)
                        .FontSize(16)
                        .LineHeight(3);
                });

                column.Item().Text(text =>
                {
                    //TODO: change ? to ! below
                    text.Span($"{order.BillingConsignment?.FamilyName}, " +
                              $"{order.BillingConsignment?.GivenName} ")
                        .Style(textStyle);

                    if (order.CustomerRewardsNumber == null) 
                        return; 
                    //TODO: change ? to ! below
                    text.Span($"(rewards # {order.CustomerRewardsNumber})")
                        .Style(textStyle)
                        .FontColor("#808080");
                });
                column.Item().Text(text =>
                {
                    //TODO: test with fake data and  change ? to ! below

                    if (order.BillingConsignment?.Line2 != null)
                    {
                        text.Span($"{order.BillingConsignment.Line2}, ")
                            .Style(textStyle);
                    }
                    //TODO: test with fake data and  change ? to ! below
                    text.Span($"{order.BillingConsignment?.Line1}, " +
                              $"{order.BillingConsignment?.Locality}. " +
                              $"{order.BillingConsignment?.Region} " +
                              $"{order.BillingConsignment?.Postcode}")
                        .Style(textStyle);

                });
                column.Item().Text(text =>
                {
                    //TODO: test with fake data and  change ? to ! below
                    text.Span("");
                });
                column.Item().Text(text =>
                {
                    //TODO: test with fake data and  change ? to ! below
                    text.Span($"{order.BillingConsignment?.Phone}")
                        .Style(textStyle);
                });
                column.Item().Text(text =>
                {
                    //TODO: test with fake data and  change ? to ! below
                    text.Span($"{order.BillingConsignment?.Email}")
                        .Style(textStyle);
                });
            });
            
            row.ConstantItem(100).Text(text =>
            {
                    text.Span(Convert.ToBoolean(
                        $"{order.Payment == null || order.Header.PaymentStatus == PaymentStatus.Validated}")
                        ? "UNPAID"
                        : $"{order.Header.PaymentStatus.ToString()}").Style(titleStyle).FontSize(16);
                    text.AlignRight();
            });
            
        });
        
    }
    public void ComposeContent(IContainer container)
    {
       
        container.PaddingVertical(20).Layers(layers =>
        {
           layers.PrimaryLayer().Column(column =>
        {
            
            column.Item().Element(ComposeTable);
            column.Item().Element(ComposeSummary);
            column.Spacing(5);
            column.Item().AlignRight()
                .Height(40)
                .Width(128)
                .Canvas((canvas, size) =>
            {
                using var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = 2,
                    IsStroke = true };
                canvas.DrawRect(5, 2, 128, 40, paint);
            });
            column.Item().Text(text =>
            {
                text.Span("Pharmacist Initials");
                text.AlignRight();
            });
            column.Item().AlignRight()
                .Height(40)
                .Width(128)
                .Canvas((canvas, size) =>
                {
                    using var paint = new SKPaint
                    {
                        Color = SKColors.Black,
                        StrokeWidth = 2,
                        IsStroke = true };
                    canvas.DrawRect(5, 2, 128, 40, paint);
                });
            column.Item().Text(text =>
            {
                text.Span("Date");
                text.AlignRight();
            });
        });
           layers
               .Layer()
               .AlignCenter()
               .AlignMiddle()
               .Canvas((canvas, size) =>
               {
                   using (var paint = new SKPaint
                          {
                              Color = new SKColor(128, 128, 128, 50), // Set color with alpha value for transparency
                              TextSize = 50,
                              IsAntialias = true,
                              TextAlign = SKTextAlign.Center,
                              
                          })
                   {
                       canvas.RotateDegrees(-35, size.Width / 2, size.Height / 2); 
                       canvas.DrawText("Internal Use Only", size.Width / 2, size.Height / 2, paint);
                   }
               });
        });

    }
    void ComposeTable(IContainer container)
    {
       
        container.Table(table =>
        {
            IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            {
                return container
                    .PaddingHorizontal(15)
                    .PaddingVertical(5);
            }
            
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(4);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
            });
            table.Header(header =>
            {
                byte[] imageData = File.ReadAllBytes("/Users/arikmir/Downloads/prescription.png");
                header.Cell().Image(imageData);
                // header.Cell().Element(CellStyle).Text("Product").Bold();
                header.Cell().Element(CellStyle).AlignRight().Text("Quantity").Bold();
                header.Cell().Element(CellStyle).AlignRight().Text("Total").Bold();
                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
            });
            
            foreach (var item in order.Items)
            {
                table.Cell().Element(CellStyle).AlignLeft().Text($"{item.Name}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{(item.PriceExcludingTax ?? 0) * item.Quantity}");
                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
            }

        });
        
    }

    void ComposeSummary(IContainer container)
    {
        container.Table(table =>
        {
            IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            {
                return container
                    .PaddingHorizontal(15)
                    .PaddingVertical(5);
            }
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(4);
                columns.RelativeColumn(1);
                columns.RelativeColumn(1);
            });
            table.Header(header =>
            {
                var subTotal = order.Total - (order.Delivery?.PriceIncludingTax ?? 0);
                var delivery = order.Delivery?.PriceIncludingTax ?? 0;
                
                header.Cell().Element(CellStyle).Text("").Bold();
                
                header.Cell().Element(CellStyle).AlignRight().Text("Subtotal");
                header.Cell().Element(CellStyle).AlignRight().Text($"${subTotal}");
                
                header.Cell().Row(2).Column(2).Element(CellStyle).AlignRight().Text("Delivery");
                header.Cell().Row(2).Column(3).Element(CellStyle).AlignRight().Text($"${delivery}");
                
                header.Cell().Row(3).Column(2).Element(CellStyle).AlignRight().Text("Total").ExtraBold().ExtraBlack();
                header.Cell().Row(3).Column(3).Element(CellStyle).AlignRight().Text($"${order.Total}").ExtraBold().ExtraBlack();
               

                IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
            });
               
        });
    }
    void ComposeFooter(IContainer container)
    {
        var currentDateTime = DateTime.Now.ToString("d/M/yyyy, h:mm tt ");
        container.Column(column =>
        {
            column.Item().AlignRight().Text(text =>
            {
                text.CurrentPageNumber().FontSize(8);
                text.Span(" / ").FontSize(8);
                text.TotalPages().FontSize(8);
            });
            column.Item().AlignLeft().Text(text =>
            {
                text.Span($"{currentDateTime}").FontSize(9);
            });
        });
    }
}
