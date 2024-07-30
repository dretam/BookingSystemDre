using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class TrxHistory
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public string Necessity { get; set; } = null!;

    public int RequestedBy { get; set; }

    public DateTime RequestedDate { get; set; }

    public DateTimeOffset TimeFrom { get; set; }

    public DateTimeOffset TimeTo { get; set; }

    public string? CanceledBy { get; set; }

    public DateTime? CanceledDate { get; set; }

    public string? Status { get; set; }

    public string? Routine { get; set; }

    public bool? IsAllDay { get; set; }

    public virtual MstUser RequestedByNavigation { get; set; } = null!;

    public virtual MstRoom Room { get; set; } = null!;
}
