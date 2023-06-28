using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FinanciarTeApi.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly IConfiguration config;
        private readonly FinanciarTeContext context;
        public ServiceLogin(FinanciarTeContext _context, IConfiguration _config)
        {
            this.context = _context;
            this.config = _config;
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<ComandoLogin> Login([FromBody] ComandoLogin comando)
        {
            ComandoLogin emailPass = new ComandoLogin();

            try
            {
                byte[] ePass = GetHash(comando.Pass);
                var activo = await context.Usuarios.FirstOrDefaultAsync(c => c.Activo);

                emailPass = await context.Usuarios
                    .Include(x => x.IdTipoUsuarioNavigation)
                    .FirstOrDefaultAsync(c => c.User == comando.User && c.Hashpass == ePass) ?? new ComandoLogin();

                if (emailPass != null)
                {
                    if (emailPass.Activo) //&& activo != null)
                    {
                        emailPass.Ok = true;
                        emailPass.CodigoEstado = 200;
                        emailPass.Error = "Es activo y valido";
                        return emailPass;
                    }
                    else
                    {
                        emailPass.Ok = false;
                        emailPass.CodigoEstado = 400;
                        emailPass.Error = ("El email no esta activo");
                        return emailPass;
                    }
                }
                else
                {
                    emailPass.Ok = false;
                    emailPass.CodigoEstado = 400;
                    emailPass.Error = ("El email o contraseña no existe");
                    return emailPass;
                }
            }
            catch (Exception ex)
            {
                emailPass.Ok = false;
                emailPass.CodigoEstado = 400;
                emailPass.Error = ex.Message;
                return emailPass;
            }
        }

        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }
    }
}
