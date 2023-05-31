using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class HistoricosIndice
{
    public long Id { get; set; }

    public DateTime Fecha { get; set; }

    public decimal ValorDolar { get; set; }

    public decimal? ValorDolarBlue { get; set; }
}
