using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class EntidadesFinanciera
{
    public long IdEntidadFinanciera { get; set; }

    public string? Descripción { get; set; }

    public string? NroCuenta { get; set; }

    public string? Cbu { get; set; }

    public string? Alias { get; set; }

    public long? IdTipoEntidad { get; set; }

    public decimal? MontoInicial { get; set; }

    public virtual TiposEntidadFinanciera? IdTipoEntidadNavigation { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
