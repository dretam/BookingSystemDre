﻿using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class ChlMenu
{
    public int Id { get; set; }

    public int MstMenuId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual MstMenu MstMenu { get; set; } = null!;
}
