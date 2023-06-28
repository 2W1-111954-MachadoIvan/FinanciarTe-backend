using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Usuario
{
    public long IdUsuarios { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public long Numero { get; set; }

    public long Telefono { get; set; }

    public long Legajo { get; set; }

    public string User { get; set; } = null!;

    public byte[] Hashpass { get; set; } = null!;

    public bool Activo { get; set; }

    public long IdTipoUsuario { get; set; }

    public virtual TiposUsuario IdTipoUsuarioNavigation { get; set; } = null!;
}
