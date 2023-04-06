using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Categoria
{
    public long IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public long? IdTipoTransaccion { get; set; }

    public virtual ICollection<DetalleTransaccione> DetalleTransacciones { get; } = new List<DetalleTransaccione>();

    public virtual TiposTransaccion? IdTipoTransaccionNavigation { get; set; }
}
