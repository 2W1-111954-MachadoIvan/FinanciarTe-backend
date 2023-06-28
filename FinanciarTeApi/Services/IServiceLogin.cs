using FinanciarTeApi.Commands;
using FinanciarTeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Services
{
    public interface IServiceLogin
    {
        Task<List<Usuario>> GetUsuarios();
        Task<ComandoLogin> Login([FromBody] ComandoLogin comando);
    }
}
