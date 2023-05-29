using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewPrestamo
{
    public long IdPrestamo { get; set; }

    public string? Cliente { get; set; }

    public long? DniCliente { get; set; }

    public decimal? IndiceFinanciarTe { get; set; }

    public long? Scoring { get; set; }

    public decimal? BeneficioScoring { get; set; }

    public long? MontoOtorgado { get; set; }

    public decimal? MontoADevolver { get; set; }

    public long? Cuotas { get; set; }

    public DateTime? VencimientoPrimeraCuota { get; set; }

    public DateTime? VencimientoUltimaCuota { get; set; }

    public int? CuotasPagas { get; set; }

    public decimal? MontoAbonado { get; set; }

    public decimal? SaldoPendiente { get; set; }

    public string Estado { get; set; } = null!;
}
