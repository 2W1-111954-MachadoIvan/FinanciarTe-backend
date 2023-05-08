namespace FinanciarTeApi.Commands
{
    public class ComandoPrestamo
    {
        public long idPrestamo { get; set; }
        public long idCliente { get; set; }
        public long montoOtorgado { get; set; }
        public long Cuotas { get; set; }
        public long DiaVencimientoCuota { get; set; }
        public long idScoring { get; set; }
        public decimal IndiceInteres { get; set; }
        public bool RefinanciaDeuda { get; set; }
        public long? IdPrestamoRefinanciado { get; set; }
        public long idTransaccion { get; set; }
        public long idEntidadFinanciera { get; set; }
        public long idCategoria { get; set; }
        public DateTime Fecha { get; set; }

    }
}
