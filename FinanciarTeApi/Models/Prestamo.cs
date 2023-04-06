using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Prestamo
{
    public long IdPrestamo { get; set; }

    public long? IdCliente { get; set; }

    public long? MontoOtorgado { get; set; }

    public long? Cuotas { get; set; }

    public long? DiaVencimientoCuota { get; set; }

    public long? IdScoring { get; set; }

    public decimal? IndiceInteres { get; set; }

    public bool? RefinanciaDeuda { get; set; }

    public long? IdPrestamoRefinanciado { get; set; }

    public long? IdTransaccion { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Scoring? IdScoringNavigation { get; set; }

    public virtual Transaccione? IdTransaccionNavigation { get; set; }
}
