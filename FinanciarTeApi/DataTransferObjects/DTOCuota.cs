namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOCuota
    {
        public long? idCuota { get; set; }
        public long? idCliente { get; set; }
        public long? idPrestamo { get; set; }
        public long? nroCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? MontoCuota { get; set; }
        public decimal? montoAbonado { get; set; }
        public DateTime? fechaPago { get; set; }
        public string? cuotaVencida { get; set; }
        public long? idTransaccion { get; set; }
        public long? idDetalleTransaccion { get; set; }
        public long? puntos { get; set; }
    }
}
