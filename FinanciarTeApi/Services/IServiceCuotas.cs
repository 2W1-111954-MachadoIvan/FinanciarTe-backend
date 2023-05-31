using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceCuotas
    {
        Task<List<ViewCuotasCliente>> GetViewCuotasByCliente(int id);
        Task<Cuota> GetCuotasByCliente(int id);
        Task<Cuota> GetCuotaByID(int id);
        Task<ResultadoBase> DeleteCuota(int id);
        Task<ResultadoBase> RegistrarPagoCuotas(ComandoCuota comando);
        Task<ResultadoBase> ModificarPagoCuotas(ComandoCuota comando);
    }
}
