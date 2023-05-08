namespace FinanciarTeApi.Commands
{
    public class ComandoCobroCuota
    {        
        public DateTime? fechaPago { get; set; }        
        public long idTransaccion { get; set; }        
        public long idEntidadFinanciera { get; set; }
        public List<ComandoDetalleCuotas>? detalleCuotas { get; set; }
    }

    public class ComandoDetalleCuotas
    {
        public long? idCobroCuota { get; set; }
        public long? idCliente { get; set; }
        public long? idPrestamo { get; set; }
        public long? numeroCuota { get; set; }
        public decimal? montoAbonado { get; set; }
        public bool? cuotaVencida { get; set; }
        public long? idPuntos { get; set; }
        public long? idTransaccion { get; set; }
        public long? idDetalleTransaccion { get; set; }
    }
}
