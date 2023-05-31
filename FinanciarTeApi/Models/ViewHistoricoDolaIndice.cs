using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewHistoricoDolaIndice
{
    public DateTime Fecha { get; set; }

    public decimal ValorDolar { get; set; }

    public decimal? ValorDolarBlue { get; set; }

    public decimal? Indice { get; set; }
}
