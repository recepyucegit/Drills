using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class Exercise
{
    public int Id { get; set; }

    public string CompoundExercise1 { get; set; } = null!;

    public string CompoundExercise2 { get; set; } = null!;

    public string CompoundExercise3 { get; set; } = null!;

    public string CompoundExercise4 { get; set; } = null!;

    public string CompoundExercise5 { get; set; } = null!;

    public string IsolationExercise1 { get; set; } = null!;

    public string IsolationExercise2 { get; set; } = null!;

    public string IsolationExercise3 { get; set; } = null!;

    public string IsolationExercise4 { get; set; } = null!;

    public string IsolationExercise5 { get; set; } = null!;

    public virtual User IdNavigation { get; set; } = null!;
}
