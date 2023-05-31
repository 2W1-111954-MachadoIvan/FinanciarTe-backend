using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewCuota
{
    public long? Dni { get; set; }

    public string? Cliente { get; set; }

    public long? IdPrestamo { get; set; }

    public long? CuotaN { get; set; }

    public string? FechaDeVencimiento { get; set; }

    public decimal? MontoDeCuota { get; set; }

    public string? FechaDePago { get; set; }

    public decimal? MontoAbonado { get; set; }

    public string CuotaVencida { get; set; } = null!;

    public long? IdTransacción { get; set; }

    public long? IdDetalleTransacción { get; set; }

    public long? PuntosOtorgados { get; set; }
}
