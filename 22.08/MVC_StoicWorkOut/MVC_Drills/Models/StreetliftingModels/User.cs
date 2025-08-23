using System;
using System.Collections.Generic;

namespace MVC_Drills.Models.StreetliftingModels;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Password { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual UserProfile? UserProfile { get; set; }
}
