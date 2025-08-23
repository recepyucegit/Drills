using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class Measurement
{
    public int Id { get; set; }

    public int Neckmeasure { get; set; }

    public int Chestmeasure { get; set; }

    public int Bellymeasure { get; set; }

    public int Quadricepsmasure { get; set; }

    public virtual BodyProgress? BodyProgress { get; set; }

    public virtual UserProfile IdNavigation { get; set; } = null!;
}
