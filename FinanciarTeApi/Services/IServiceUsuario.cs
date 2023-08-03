using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;

namespace FinanciarTeApi.Services
{
    public interface IServiceUsuario
    {
        Task<List<Usuario>> GetUsuarios();
        Task<List<DTOUsuario>> GetViewUsuarios();
        Task<Usuario> GetUsuarioByID(long legajo);
        Task<Usuario> GetUsuarioByUser(string user);
        Task<ResultadoBase> DeleteUsuario(int id);
    }
}
