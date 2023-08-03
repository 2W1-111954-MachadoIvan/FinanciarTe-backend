using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;

namespace FinanciarTeApi.Services
{
    public interface IServicePuntos
    {
        Task<List<DTOPuntos>> GetHistoricoPuntos();
        Task<List<DTOPuntos>> GetHistoricoPuntosByCliente(long id);
    }
}
