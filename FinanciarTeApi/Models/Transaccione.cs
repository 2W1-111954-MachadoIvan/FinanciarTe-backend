using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Transaccione
{
    public long IdTransaccion { get; set; }

    public long? IdEntidadFinanciera { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public bool? Anulada { get; set; }

    public string? MotivoAnulacion { get; set; }

    public virtual ICollection<DetalleTransaccione> DetalleTransacciones { get; } = new List<DetalleTransaccione>();

    public virtual EntidadesFinanciera? IdEntidadFinancieraNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; } = new List<Prestamo>();
}
