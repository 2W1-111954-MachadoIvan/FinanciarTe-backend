using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewResumenCliente
{
    public string? Nombre { get; set; }

    public long? Cliente { get; set; }

    public int? CantidadDePrestamos { get; set; }

    public int? PrestamosCancelados { get; set; }

    public int? PrestamosPendientes { get; set; }

    public int? PrestamosRefinanciados { get; set; }

    public int? CuotasVencidas { get; set; }

    public int? TotalDeCuotas { get; set; }

    public int? CumplimientoCuotas { get; set; }
}
