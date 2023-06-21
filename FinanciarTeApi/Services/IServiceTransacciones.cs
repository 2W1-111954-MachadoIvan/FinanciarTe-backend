using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceTransacciones
    {
        Task<ResultadoBase> RegistrarTransaccion(ComandoTransaccion transaccion);
        Task<ResultadoBase> ModificarTransaccion(ComandoTransaccion transaccion);
        Task<List<DTOTransacciones>> GetListadoTransacciones();
        Task<DTOTransacciones_DetTr> GetTransaccionById(int id);
        Task<ResultadoBase> DeleteSoftTransaccion(ComandoAnulaciones anulacion);
    }
}
