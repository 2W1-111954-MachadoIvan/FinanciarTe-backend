using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public class ServicePrestamo : IServicePrestamo
    {
        public Task<ResultadoBase> DeleteCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOPrestamo>> GetPrestamosByCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DTOPrestamo> GetPrestamosByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultadoBase> PostPrestamo(ComandoPrestamo comando)
        {
            throw new NotImplementedException();
        }

        public Task<ResultadoBase> PutPrestamo(ComandoPrestamo comando)
        {
            throw new NotImplementedException();
        }
    }
}
