namespace FinanciarTeApi.DataTransferObjects
{
    public class DTODolarIndice
    {
        public DateTime Fecha { get; set; }
        public decimal ValorDolar { get; set; }
        public decimal? ValorDolarBlue { get; set; }
        public decimal? Indice { get; set; }
    }
}
