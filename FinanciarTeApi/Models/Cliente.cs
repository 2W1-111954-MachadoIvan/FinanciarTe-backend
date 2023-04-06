using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class Cliente
{
    public long NroDni { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public DateTime? FechaDeNacimiento { get; set; }

    public long? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Dirección { get; set; }

    public long? Numero { get; set; }

    public long? IdCiudad { get; set; }

    public long? CodigoPostal { get; set; }

    public long? PuntosIniciales { get; set; }

    public long? IdContactoAlternativo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<CobrosCuota> CobrosCuota { get; } = new List<CobrosCuota>();

    public virtual Ciudade? IdCiudadNavigation { get; set; }

    public virtual ContactosAlternativo? IdContactoAlternativoNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; } = new List<Prestamo>();
}
