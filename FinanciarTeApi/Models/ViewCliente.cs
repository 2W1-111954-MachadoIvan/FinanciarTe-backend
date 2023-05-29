using System;
using System.Collections.Generic;

namespace FinanciarTeApi.Models;

public partial class ViewCliente
{
    public long Dni { get; set; }

    public string? Apellidos { get; set; }

    public string? Nombres { get; set; }

    public DateTime? FechaDeNacimiento { get; set; }

    public long? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Dirección { get; set; }

    public string? Ciudad { get; set; }

    public string? Provincia { get; set; }

    public long? CodigoPostal { get; set; }

    public string? ContactoAlternativo { get; set; }

    public long? TelefonoContactoAlternativo { get; set; }

    public string? EmailContactoAlternativo { get; set; }

    public string Activo { get; set; } = null!;

    public long? PuntosIniciales { get; set; }

    public int? CantidadDePrestamos { get; set; }

    public long? PuntosActuales { get; set; }

    public int Scoring { get; set; }

    public decimal? BeneficioScoring { get; set; }
}
