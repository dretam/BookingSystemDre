using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class TrxCatering
{
    public int Id { get; set; }

    public string Item { get; set; } = null!;

    public int BookingRoomId { get; set; }

    public int RoomId { get; set; }

    public int? Quantity { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public int RequestedBy { get; set; }

    public virtual MstRoom BookingRoom { get; set; } = null!;

    public virtual MstUser RequestedByNavigation { get; set; } = null!;

    public virtual MstRoom Room { get; set; } = null!;
}
