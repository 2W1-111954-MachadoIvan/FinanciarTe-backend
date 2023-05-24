using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceCliente
    {
        Task<List<DTOCliente>> GetClientes();
        Task<ResultadoBase> PostCliente(ComandoCliente cliente);
        Task<DTOCliente> GetViewClienteByID(int id);
        Task<ComandoCliente> GetClienteByID(int id);
        Task<ResultadoBase> PutCliente(ComandoCliente cliente);
        Task<ResultadoBase> DeleteCliente(int id);
        Task<List<ComboBoxItemDto>> GetClientesForComboBox();
    }
}
