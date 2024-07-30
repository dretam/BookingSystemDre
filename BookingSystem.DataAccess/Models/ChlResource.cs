using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class ChlResource
{
    public string ResourceCode { get; set; } = null!;

    public bool? Status { get; set; }

    public int ResourceId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual MstResource Resource { get; set; } = null!;

    public virtual ICollection<TrxMstRoomChlResource> TrxMstRoomChlResources { get; set; } = new List<TrxMstRoomChlResource>();
}
