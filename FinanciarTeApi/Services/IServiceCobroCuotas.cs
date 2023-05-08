using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceCobroCuotas
    {
        Task<List<DTOCobroCuota>> GetCuotasByCliente(int id);
        Task<DTOCobroCuota> GetCuotaByID(int id);
        Task<ResultadoBase> DeleteCuota(int id);
        Task<ResultadoBase> RegistrarCuotas(ComandoCobroCuota comando);
        Task<ResultadoBase> ModificarCuota(ComandoCobroCuota comando);
    }
}
