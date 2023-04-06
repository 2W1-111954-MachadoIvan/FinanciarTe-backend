using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class TiposUsuario
{
    public long IdTipoUsuario { get; set; }

    public string? Descripción { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
