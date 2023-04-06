using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Provincia
{
    public long IdProvincia { get; set; }

    public string? Provincia1 { get; set; }

    public virtual ICollection<Ciudade> Ciudades { get; } = new List<Ciudade>();
}
