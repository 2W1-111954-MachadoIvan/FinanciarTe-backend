using FinanciarTeApi.Commands;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using FinanciarTeApi.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanciarTeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IServiceRegistro servicio;

        public RegistroController(IServiceRegistro _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpPost("PostRegister")]
        public async Task<ActionResult<ResultadoBase>> PostRegister([FromBody] ComandoRegistro comando)
        {
            Usuario r = new Usuario();

            byte[] ePass = GetHash(comando.Pass);

            r.Nombres = comando.Nombre;
            r.Apellidos = comando.Apellido;
            r.Calle = comando.Calle;
            r.Numero = comando.Numero;
            r.Telefono = comando.Telefono;
            r.Legajo = comando.Legajo;
            r.User = comando.User;
            r.Hashpass = ePass;
            r.Activo = true;
            r.IdTipoUsuario = comando.idTipoUsuario;

            return Ok(await this.servicio.PostRegister(r));
        }

        [HttpPut("PutUsuario")]
        public async Task<ActionResult<ResultadoBase>> PutUsuario([FromBody] ComandoPutUsuario comando)
        {

            Usuario r = new Usuario();

            byte[] ePass = GetHash(comando.PassNueva);

            r.Nombres = comando.Nombre;
            r.Apellidos = comando.Apellido;
            r.Calle = comando.Calle;
            r.Numero = comando.Numero;
            r.Telefono = comando.Telefono;
            r.IdTipoUsuario = comando.idTipoUsuario;
            r.Legajo = comando.Legajo;
            r.User = comando.User;
            r.Hashpass = ePass;
            r.Activo = true;


            return Ok(await this.servicio.PutUsuario(r));
        }

        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }
    }
}
