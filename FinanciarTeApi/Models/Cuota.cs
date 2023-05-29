using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Cuota
{
    public long IdCuota { get; set; }

    public long? IdCliente { get; set; }

    public long? IdPrestamo { get; set; }

    public long? NumeroCuota { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public decimal? MontoCuota { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal? MontoAbonado { get; set; }

    public bool? CuotaVencida { get; set; }

    public long? IdTransaccion { get; set; }

    public long? IdDetalleTransaccion { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Prestamo? IdPrestamoNavigation { get; set; }
}
