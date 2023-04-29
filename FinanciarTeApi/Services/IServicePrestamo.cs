using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServicePrestamo
    {
        Task<List<DTOPrestamo>> GetPrestamosByCliente(int id);
        Task<DTOPrestamo> GetPrestamosByID(int id);
        Task<ResultadoBase> DeleteCliente(int id);
        Task<ResultadoBase> PostPrestamo(ComandoPrestamo comando);
        Task<ResultadoBase> PutPrestamo(ComandoPrestamo comando);
    }
}
