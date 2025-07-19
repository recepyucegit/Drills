using System;
using System.Collections.Generic;

namespace CA_EFSorgular.Models.Northwind;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
