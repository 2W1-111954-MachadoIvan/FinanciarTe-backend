namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOListadoPrestamos
    {
        public long idPrestamo { get; set; }
        public string? Cliente { get; set; }
        public long? DniCliente { get; set; }
        public decimal? IndiceFinanciarTe { get; set; }
        public long? Scoring { get; set; }
        public decimal? BeneficioScoring { get; set; }
        public long? MontoOtorgado { get; set; }
        public decimal? MontoADevolver { get; set; }
        public long? Cuotas { get; set; }
        public DateTime? VencimientoPrimeraCuota { get; set; }
        public DateTime? VencimientoUltimaCuota { get; set; }
        public int? CuotasPagas { get; set; }
        public decimal? MontoAbonado { get; set; }
        public decimal? SaldoPendiente { get; set; }
        public string Estado { get; set; } = null!;
    }

    public class DTOPrestamo
    {
        public long idPrestamo { get; set; }
        public long? idCliente { get; set; }
        public long? montoOtorgado { get; set; }
        public string estado { get; set; }
        public decimal? saldoPendiente { get; set; }
        public List<DTOCuota>? cuotas { get; set; } = new List<DTOCuota>();
    }
}
