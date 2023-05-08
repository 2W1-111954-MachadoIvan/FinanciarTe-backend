namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOListadoPrestamos
    {
        public long idPrestamo { get; set; }
        public long? idCliente { get; set; }
        public long? montoOtorgado { get; set; }
        public string estado { get; set; }
        public string coutasPagas { get; set; }
        public decimal? saldoPendiente { get; set; }
    }

    public class DTOPrestamo
    {
        public long idPrestamo { get; set; }
        public long? idCliente { get; set; }
        public long? montoOtorgado { get; set; }
        public string estado { get; set; }
        public decimal? saldoPendiente { get; set; }
        public List<DTOCobroCuota>? cuotas { get; set; } = new List<DTOCobroCuota>();
    }
}
