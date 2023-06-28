using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class DetalleTransaccione
{
    public long IdDetalleTransacciones { get; set; }

    public long? IdCategoria { get; set; }

    public string? Detalle { get; set; }

    public decimal? Monto { get; set; }

    public long? IdTransaccion { get; set; }

    public bool? Anulado { get; set; }

    public string? MotivoAnulacion { get; set; }

    public virtual ICollection<Cuota> Cuota { get; } = new List<Cuota>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Transaccione? IdTransaccionNavigation { get; set; }

    public virtual ICollection<PuntosPorCliente> PuntosPorClientes { get; } = new List<PuntosPorCliente>();
}
