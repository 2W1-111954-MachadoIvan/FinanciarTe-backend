namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOUsuario
    {
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Calle { get; set; }

        public long? Numero { get; set; }

        public long? Telefono { get; set; }

        public long? Legajo { get; set; }

        public string User { get; set; }

        public string Activo { get; set; }

        public string tipoUsuario { get; set; }
    }
}
