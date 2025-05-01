using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class MaterialType
{
    public int Id { get; set; }

    public string MaterialType1 { get; set; } = null!;

    public string PercentageOfMaterialDefects { get; set; } = null!;
}
