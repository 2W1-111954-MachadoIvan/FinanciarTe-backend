namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOTransacciones
    {
        public long idTransaccion { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public string EntidadFinanciera { get; set; }
        public decimal? MontoTotal { get; set; }
    }
}
