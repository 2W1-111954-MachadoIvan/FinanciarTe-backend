using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceCobroCuotas
    {
        Task<List<DTOPrestamo>> GetCuotasByCliente(int id);
        Task<DTOPrestamo> GetCuotaByID(int id);
        Task<ResultadoBase> DeleteCuota(int id);
        Task<ResultadoBase> PostCuota(ComandoPrestamo comando);
        Task<ResultadoBase> PutCuota(ComandoPrestamo comando);
    }
}
