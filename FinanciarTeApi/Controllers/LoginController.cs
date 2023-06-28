using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanciarTeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IServiceLogin servicio;

        public LoginController(IServiceLogin _servicio, IConfiguration config, FinanciarTeContext context)
        {
            this.servicio = _servicio;
            _config = config;
        }

        [HttpGet("GetUsuarios")]
        public async Task<ActionResult> GetUsuarios()
        {
            var get = await this.servicio.GetUsuarios();
            return Ok(get);
        }

        [HttpPost("PostLogin")]
        public async Task<IActionResult> Login([FromBody] ComandoLogin comando)
        {
            var resultado = await this.servicio.Login(comando);

            if (resultado.Ok)
            {
                var claims = new[]
                {
                     new Claim(ClaimTypes.NameIdentifier, resultado.IdUsuario.ToString()),
                     new Claim(ClaimTypes.Name, resultado.User),
                     new Claim(ClaimTypes.Role, string.Join(",", resultado.tipoUsuario))
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(double.Parse(_config.GetSection("AppSettings:Expires").Value)),
                    SigningCredentials = creds,

                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                resultado.Token = tokenHandler.WriteToken(token);

                return Ok(resultado);
            }

            return Unauthorized(resultado);
        }
    }
}
