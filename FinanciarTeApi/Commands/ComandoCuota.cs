namespace FinanciarTeApi.Commands
{
    public class ComandoCuota
    {        
        public DateTime? fechaPago { get; set; }        
        public long idTransaccion { get; set; }        
        public long idEntidadFinanciera { get; set; }
        public List<ComandoDetalleCuotas>? detalleCuotas { get; set; }
    }

    public class ComandoDetalleCuotas
    {
        public long IdCuota { get; set; }
        public long? IdPrestamo { get; set; }

        public long? NumeroCuota { get; set; }

        public DateTime? FechaPago { get; set; }

        public decimal? MontoAbonado { get; set; }

        public bool? CuotaVencida { get; set; }
        public long? IdTransaccion { get; set; }

        public long? IdDetalleTransaccion { get; set; }
    }
}
