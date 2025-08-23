using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class LeaderBoard
{
    public int Id { get; set; }

    public int Rate { get; set; }

    public string Comments { get; set; } = null!;

    public int Pullup { get; set; }

    public int Dips { get; set; }

    public int Chinup { get; set; }

    public int MuscleUp { get; set; }

    public int Squat { get; set; }

    public int Deadlift { get; set; }

    public int BenchPress { get; set; }

    public virtual UserProfile IdNavigation { get; set; } = null!;
}
