using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;

namespace FinanciarTeApi.Services
{
    public interface IServiceUsuario
    {
        Task<List<Usuario>> GetUsuarios();
        Task<List<DTOUsuario>> GetViewUsuarios();
    }
}
