﻿using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Transaccione
{
    public long IdTransaccion { get; set; }

    public long? IdEntidadFinanciera { get; set; }

    public long? IdDetalleTransacciones { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public virtual ICollection<CobrosCuota> CobrosCuota { get; } = new List<CobrosCuota>();

    public virtual DetalleTransaccione? IdDetalleTransaccionesNavigation { get; set; }

    public virtual EntidadesFinanciera? IdEntidadFinancieraNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; } = new List<Prestamo>();
}