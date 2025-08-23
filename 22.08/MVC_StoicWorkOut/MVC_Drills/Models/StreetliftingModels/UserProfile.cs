using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class UserProfile
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual LeaderBoard? LeaderBoard { get; set; }

    public virtual LiftProgress? LiftProgress { get; set; }

    public virtual Measurement? Measurement { get; set; }
}
