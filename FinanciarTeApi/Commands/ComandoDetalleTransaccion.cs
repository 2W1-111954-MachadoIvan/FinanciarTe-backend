namespace FinanciarTeApi.Commands
{
    public class ComandoDetalleTransaccion
    {
        public long idDetalleTransaccion { get; set; }
        public long idCategoria { get; set; }
        public string detalle { get; set; }
        public decimal monto { get; set; }
        public long idTransaccion { get; set; }

    }
}
