namespace FinanciarTeApi.Commands
{
    public class ComandoTransaccion
    {
        public int idTransaccion { get; set; }
        public int idEntidadFinanciera { get; set; }
        public DateTime fechaTransaccion { get; set; }

        public List<ComandoDetalleTransaccion> detallesTransacciones { get; set;}

    }
}
