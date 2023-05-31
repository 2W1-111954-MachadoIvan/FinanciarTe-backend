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

    public class ViewCuotasCliente
    {
        public long? Dni { get; set; }
        public string? Cliente { get; set; }
        public long? IdPrestamo { get; set; }
        public long? CuotaN { get; set; }
        public string? FechaDeVencimiento { get; set; }
        public decimal? MontoDeCuota { get; set; }
        public string? FechaDePago { get; set; }
        public decimal? MontoAbonado { get; set; }
        public string CuotaVencida { get; set; } = null!;
        public long? IdTransacción { get; set; }
        public long? IdDetalleTransacción { get; set; }
        public long? PuntosOtorgados { get; set; }
    }
}
