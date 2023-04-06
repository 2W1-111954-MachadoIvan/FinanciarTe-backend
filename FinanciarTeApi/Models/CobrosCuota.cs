using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class CobrosCuota
{
    public long IdCobroCuota { get; set; }

    public long? IdCliente { get; set; }

    public long? IdPrestamo { get; set; }

    public decimal? MontoAbonado { get; set; }

    public DateTime? FechaPago { get; set; }

    public bool? CuotaVencida { get; set; }

    public long? IdTransaccion { get; set; }

    public long? IdPuntos { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Punto? IdPuntosNavigation { get; set; }

    public virtual Transaccione? IdTransaccionNavigation { get; set; }
}
