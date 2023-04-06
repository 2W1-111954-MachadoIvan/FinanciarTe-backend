using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class TiposTransaccion
{
    public long IdTipoTransaccion { get; set; }

    public string? Descripción { get; set; }

    public virtual ICollection<Categoria> Categoria { get; } = new List<Categoria>();
}
