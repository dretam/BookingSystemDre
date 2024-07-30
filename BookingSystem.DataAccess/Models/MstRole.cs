using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<MstMenu> MstMenus { get; set; } = new List<MstMenu>();

    public virtual ICollection<MstUser> MstUsers { get; set; } = new List<MstUser>();
}
