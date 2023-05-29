using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewTransaccionesConDet
{
    public long IdTransaccion { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public string? Descripción { get; set; }

    public long? IdDetalleTransacciones { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public decimal? Monto { get; set; }
}
