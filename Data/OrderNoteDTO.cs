using System;

namespace pdfPOC;

public class OrderNoteDTO
{
    public string NoteId { get; set; } = default!;
    public string? CreatedByUserId { get; set; }
    public string CreatedByName { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
   // public IOrderNoteContent Content { get; set; } = default!;
}