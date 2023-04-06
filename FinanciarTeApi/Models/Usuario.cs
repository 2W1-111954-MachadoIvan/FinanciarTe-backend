using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Usuario
{
    public long IdUsuarios { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Usuario1 { get; set; }

    public long? Legajo { get; set; }

    public bool? Activo { get; set; }

    public long? IdTipoUsuario { get; set; }

    public virtual TiposUsuario? IdTipoUsuarioNavigation { get; set; }
}
