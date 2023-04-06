using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Scoring
{
    public long IdScoring { get; set; }

    public long? Descripción { get; set; }

    public long? Puntos { get; set; }

    public decimal? Beneficio { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; } = new List<Prestamo>();
}
