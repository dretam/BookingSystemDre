using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstRoom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Floor { get; set; }

    public string? Description { get; set; }

    public int? Capacity { get; set; }

    public string? Color { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public int LocationId { get; set; }

    public virtual MstLocation Location { get; set; } = null!;

    public virtual ICollection<TrxCatering> TrxCateringBookingRooms { get; set; } = new List<TrxCatering>();

    public virtual ICollection<TrxCatering> TrxCateringRooms { get; set; } = new List<TrxCatering>();

    public virtual ICollection<TrxHistory> TrxHistories { get; set; } = new List<TrxHistory>();

    public virtual ICollection<TrxMstRoomChlResource> TrxMstRoomChlResources { get; set; } = new List<TrxMstRoomChlResource>();
}
