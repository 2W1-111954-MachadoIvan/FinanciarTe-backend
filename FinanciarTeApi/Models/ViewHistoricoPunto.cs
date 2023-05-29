using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewHistoricoPunto
{
    public string? Cliente { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Detalle { get; set; }

    public string? Descripción { get; set; }

    public long? Puntos { get; set; }
}
