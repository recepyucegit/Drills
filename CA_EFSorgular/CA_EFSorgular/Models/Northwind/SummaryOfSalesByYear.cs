﻿using System;
using System.Collections.Generic;

namespace CA_EFSorgular.Models.Northwind;

public partial class SummaryOfSalesByYear
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
