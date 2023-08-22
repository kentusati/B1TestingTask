using System;
using System.Collections.Generic;

namespace Task2.Entities;

public partial class Balance
{
    public int Id { get; set; }

    public int AccountNumber { get; set; }

    public decimal? IbAssets { get; set; }

    public decimal? IbPassive { get; set; }

    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }
}
