using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class PuntosPorCliente
{
    public long IdPuntosAsignados { get; set; }

    public long? IdCliente { get; set; }

    public long? IdTransaccion { get; set; }

    public long? IdDetalleTransaccion { get; set; }

    public long? IdPuntaje { get; set; }

    public bool? Anulado { get; set; }

    public string? MotivoAnulacion { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual DetalleTransaccione? IdDetalleTransaccionNavigation { get; set; }

    public virtual Puntaje? IdPuntajeNavigation { get; set; }

    public virtual Transaccione? IdTransaccionNavigation { get; set; }
}
