using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Puntaje
{
    public long IdPuntos { get; set; }

    public string? Descripción { get; set; }

    public long? CantidadPuntos { get; set; }

    public virtual ICollection<PuntosPorCliente> PuntosPorClientes { get; } = new List<PuntosPorCliente>();
}
