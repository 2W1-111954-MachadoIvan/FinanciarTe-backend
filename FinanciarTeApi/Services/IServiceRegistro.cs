using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Services
{
    public interface IServiceRegistro
    {
        Task<ResultadoBase> PostRegister([FromBody] Usuario u);
        Task<ResultadoBase> PutUsuario([FromBody] Usuario u);
    }
}
