namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOBalance
    {
        public long? idEntidadFinanciera { get; set; }

        public string Descripcion { get; set; }

        public decimal? MontoInicial { get; set; }

        public decimal? MontoActual { get; set; }
    }
}
