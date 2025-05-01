using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MasterPol.Models;

public partial class Partner
{
    public int IdPartner { get; set; }

    public string TypePartner { get; set; } = null!;

    public string PartnerName { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string LegalAddress { get; set; } = null!;

    public long Tin { get; set; }

    public byte Rating { get; set; }

    public int TotalProductSold => PartnerProducts?.Sum(p => p.NumberOfProducts) ?? 0;

    public int DiscountPercent => TotalProductSold switch
    {
        >= 300000 => 15,
        >= 50000 => 10,
        >= 10000 => 5,
        _ => 0
    };

    public string DiscountPartner => $"{DiscountPercent}%";

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
}
