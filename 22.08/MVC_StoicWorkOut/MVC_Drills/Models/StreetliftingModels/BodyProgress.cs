using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class BodyProgress
{
    public int Id { get; set; }

    public int BodyWeight { get; set; }

    public int BodyHeight { get; set; }

    public int BodyFat { get; set; }

    public string? Photo1 { get; set; }

    public string? Photo2 { get; set; }

    public string? Photo3 { get; set; }

    public virtual Measurement IdNavigation { get; set; } = null!;
}
