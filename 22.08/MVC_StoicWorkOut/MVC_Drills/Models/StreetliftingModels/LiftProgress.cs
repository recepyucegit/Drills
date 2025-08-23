using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class LiftProgress
{
    public int Id { get; set; }

    public double Pullup { get; set; }

    public double Dips { get; set; }

    public double Squat { get; set; }

    public virtual UserProfile IdNavigation { get; set; } = null!;
}
