﻿using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstResource
{
    public int Id { get; set; }

    public string ResourceName { get; set; } = null!;

    public bool? Status { get; set; }

    public string? IconPath { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<ChlResource> ChlResources { get; set; } = new List<ChlResource>();
}
