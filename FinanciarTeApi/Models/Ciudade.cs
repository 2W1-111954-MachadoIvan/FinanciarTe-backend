using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Ciudade
{
    public long IdCiudad { get; set; }

    public string? Ciudad { get; set; }

    public long? IdProvincia { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();

    public virtual Provincia? IdProvinciaNavigation { get; set; }
}
