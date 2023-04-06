using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ContactosAlternativo
{
    public long IdContactoAlternativo { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public long? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();
}
