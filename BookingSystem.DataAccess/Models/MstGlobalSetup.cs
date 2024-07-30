using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstGlobalSetup
{
    public int Id { get; set; }

    public string ParameterCode { get; set; } = null!;

    public string ParameterName { get; set; } = null!;

    public string ParameterDesc { get; set; } = null!;

    public string ParameterValue { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }
}
