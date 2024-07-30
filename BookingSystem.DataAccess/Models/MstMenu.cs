using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstMenu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RoleId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ChlMenu> ChlMenus { get; set; } = new List<ChlMenu>();

    public virtual MstRole Role { get; set; } = null!;
}
