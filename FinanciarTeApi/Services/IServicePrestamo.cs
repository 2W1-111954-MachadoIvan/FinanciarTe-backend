using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServicePrestamo
    {
        Task<List<DTOListadoPrestamos>> GetPrestamosByCliente(int id);
        Task<DTOPrestamo> GetPrestamoByID(int id);
        Task<ResultadoBase> DeletePrestamo(int id);
        Task<ResultadoBase> RegistrarPrestamo(ComandoPrestamo comando);
        Task<ResultadoBase> ModificarPrestamo(ComandoPrestamo comando);
    }
}
