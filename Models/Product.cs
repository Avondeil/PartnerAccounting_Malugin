using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Product
{
    public int Article { get; set; }

    public int ProductType { get; set; }

    public string NameProduct { get; set; } = null!;

    public decimal MinimumCostPartner { get; set; }
    
    public int Discount { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual ProductType ProductTypeNavigation { get; set; } = null!;


    
}
