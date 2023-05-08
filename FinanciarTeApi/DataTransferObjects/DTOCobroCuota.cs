namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOCobroCuota
    {
        public long? idCobroCuota { get; set; }
        public long? idCliente { get; set; }
        public long? idPrestamo { get; set; }
        public long? nroCuota { get; set; }
        public decimal? montoAbonado { get; set; }
        public DateTime? fechaPago { get; set; }
        public string? cuotaVencida { get; set; }
        public long? idTransaccion { get; set; }
        public long? puntos { get; set; }
    }
}
