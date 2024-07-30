using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstUser
{
    public int Id { get; set; }

    public string LoginName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual MstRole Role { get; set; } = null!;

    public virtual ICollection<TrxCatering> TrxCaterings { get; set; } = new List<TrxCatering>();

    public virtual ICollection<TrxHistory> TrxHistories { get; set; } = new List<TrxHistory>();
}
