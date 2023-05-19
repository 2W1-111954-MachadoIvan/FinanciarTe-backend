using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Punto
{
    public long IdPuntos { get; set; }

    public string? Descripción { get; set; }

    public long? CantidadPuntos { get; set; }
}
