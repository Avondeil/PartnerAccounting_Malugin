using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class PartnerProduct
{
    public int IdPartnerProduct { get; set; }

    public int Product { get; set; }

    public int Partner { get; set; }

    public int NumberOfProducts { get; set; }

    public DateOnly DateOfSale { get; set; }

    public virtual Partner PartnerNavigation { get; set; } = null!;

    public virtual Product ProductNavigation { get; set; } = null!;
}
