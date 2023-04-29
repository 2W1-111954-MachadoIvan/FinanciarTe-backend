using FinanciarTeApi.Commands;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceDetalleTransacciones
    {
        Task<ResultadoBase> RegistrarDetalleTransaccion();
        Task<List<Transaccione>> GetListadoDetalleTransacciones();
        Task<ResultadoBase> GetDetallesTransacciones(int id);
    }
}
