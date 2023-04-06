using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class TiposEntidadFinanciera
{
    public long IdTipoEntidad { get; set; }

    public string? Descripción { get; set; }

    public virtual ICollection<EntidadesFinanciera> EntidadesFinancieras { get; } = new List<EntidadesFinanciera>();
}
