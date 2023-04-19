using FinanciarTeApi.Models;

namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOCliente
    {
        public long NroDni { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public DateTime? FechaDeNacimiento { get; set; }

        public long? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public long? Numero { get; set; }

        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }

        public long? CodigoPostal { get; set; }
        public bool? Activo { get; set; }

        public long? PuntosIniciales { get; set; }

        public string? ContactoAlternativo { get; set; }
        public string? EmailContAlt { get; set; }
        public long? TelContAlt { get; set; }
    }
}
