using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FinanciarTeApi.Services
{
    public class ServiceRegistro : IServiceRegistro
    {
        private readonly FinanciarTeContext context;
        private readonly IServiceSecurity _serviceSecurity;

        public ServiceRegistro(FinanciarTeContext _context, IServiceSecurity securityService)
        { 
            this.context = _context;
            _serviceSecurity = securityService; 
        }

        public async Task<ResultadoBase> PostRegister(Usuario u)
        {
            ResultadoBase resultado = new ResultadoBase();

            if (this.ValidarUser(u.User))
            {
                if (await this.ValidarLegajo(u.Legajo))
                {

                    try
                    {
                        await context.AddAsync(u);
                        await context.SaveChangesAsync(/*_serviceSecurity.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName*/);
                        resultado.Ok = true;
                        resultado.CodigoEstado = 200;
                        return resultado;

                    }
                    catch (Exception)
                    {
                        resultado.Ok = false;
                        resultado.CodigoEstado = 400;
                        resultado.Error = "Error al registrar un usuario";
                        return resultado;
                    }
                }
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Error = "El legajo ya pertenece a un usuario";
                return resultado;
            }
            resultado.Ok = false;
            resultado.CodigoEstado = 400;
            resultado.Error = "Ya existe el correo ingresado";
            return resultado;

        }

        private bool ValidarUser(string user)
        {
            var usuario = context.Usuarios.Where(c => c.User.Equals(user)).FirstOrDefault();
            if (usuario != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private async Task<bool> ValidarLegajo(long? legajo)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(c => c.Legajo == legajo);
            if (usuario != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool validarExpresion(string user)
        {
            return user != null && Regex.IsMatch(user, "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$");
        }

        public async Task<ResultadoBase> PutUsuario(Usuario u)
        {
            ResultadoBase resultado = new ResultadoBase();
            var usuario = await context.Usuarios.Where(c => c.Legajo.Equals(u.Legajo)).FirstOrDefaultAsync();
            try
            {
                if (usuario != null)
                {
                    usuario.User = u.User;
                    usuario.Nombres = u.Nombres;
                    usuario.Apellidos = u.Apellidos;
                    usuario.Hashpass = u.Hashpass;
                    context.Update(usuario);
                    await context.SaveChangesAsync(/*this._serviceSecurity.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName*/);
                    resultado.Ok = false;
                    resultado.CodigoEstado = 200;
                    resultado.Error = "Usuario actualizado";
                    return resultado;
                }

                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Error = "Error al actualizar un usuario";
                return resultado;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
