using FinanciarTeApi.Models;

namespace FinanciarTeApi.Commands
{
    public class ComandoCliente
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

        public bool? Activo { get; set; }

        public virtual ContactosAlternativo? IdContactoAlternativoNavigation { get; set; }

    }
}
