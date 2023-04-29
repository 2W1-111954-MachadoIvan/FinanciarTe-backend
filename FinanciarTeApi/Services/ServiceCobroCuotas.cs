using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public class ServiceCobroCuotas : IServiceCobroCuotas
    {
        public Task<ResultadoBase> DeleteCuota(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DTOPrestamo> GetCuotaByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOPrestamo>> GetCuotasByCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultadoBase> PostCuota(ComandoPrestamo comando)
        {
            throw new NotImplementedException();
        }

        public Task<ResultadoBase> PutCuota(ComandoPrestamo comando)
        {
            throw new NotImplementedException();
        }
    }
}
