using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class DetalleTransaccione
{
    public long IdDetalleTransacciones { get; set; }

    public long? IdCategoria { get; set; }

    public string? Detalle { get; set; }

    public decimal? Monto { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
