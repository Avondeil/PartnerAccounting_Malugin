using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class ProductType
{
    public int IdTypeProduct { get; set; }

    public string ProductTypeName { get; set; } = null!;

    public decimal ProductTypeFactor { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
