using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;
using Newtonsoft.Json.Linq;

namespace FinanciarTeApi.Services
{
    public interface IServiceDolar
    {
        Task<List<DTODolarIndice>> GetValoresHistoricosDolar();
        Task<DTODolarIndice> GetUltimoValorDolar();
    }
}
