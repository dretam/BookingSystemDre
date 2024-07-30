using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class TrxMstRoomChlResource
{
    public int Id { get; set; }

    public string ChlResId { get; set; } = null!;

    public int MstRoomId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ChlResource ChlRes { get; set; } = null!;

    public virtual MstRoom MstRoom { get; set; } = null!;
}
