using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceCuotas
    {
        Task<List<DTOCuota>> GetCuotasByCliente(int id);
        Task<DTOCuota> GetCuotaByID(int id);
        Task<ResultadoBase> DeleteCuota(int id);
        Task<ResultadoBase> RegistrarPagoCuotas(ComandoCuota comando);
        Task<ResultadoBase> ModificarPagoCuotas(ComandoCuota comando);
    }
}
