using FinanciarTeApi.Models;

namespace FinanciarTeApi.DataTransferObjects
{
    public class DTOTransacciones_DetTr
    {
        public long IdTransaccion { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public long? idEntidadFinanciera { get; set; }
        public string EntidadFinanciera { get; set; }

        public virtual ICollection<DTO_Detalle_Transaccion> detalleTransacciones { get; } = new List<DTO_Detalle_Transaccion>();

    }

    public class DTO_Detalle_Transaccion
    {
        public long idDetalleTransaccion { get; set; }
        public long? idCategoria { get; set; }
        public string Categoria { get; set; }
        public string Detalle { get; set; }
        public string TipoTransaccion { get; set; }
        public decimal? Monto { get; set; }
        public long? IdTransaccion { get; set; }

    }
}
