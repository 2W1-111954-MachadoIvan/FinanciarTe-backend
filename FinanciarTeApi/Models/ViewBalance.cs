using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewBalance
{
    public long IdEntidadFinanciera { get; set; }

    public string? Descripción { get; set; }

    public decimal? MontoInicial { get; set; }

    public decimal? MontoActual { get; set; }
}
