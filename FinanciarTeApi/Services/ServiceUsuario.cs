using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly FinanciarTeContext _context;

        public ServiceUsuario(FinanciarTeContext context)
        {
            _context = context;
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            var query = _context.Usuarios
                                .AsNoTracking()
                                .Select(u => new Usuario
                                {
                                    Nombres = u.Nombres,
                                    Apellidos = u.Apellidos,
                                    Legajo = u.Legajo,
                                    Telefono = u.Telefono,
                                    Calle = u.Calle,
                                    Numero = u.Numero,
                                    IdTipoUsuario = u.IdTipoUsuario,
                                    User = u.User,
                                    Activo = u.Activo,
                                }).ToListAsync();

            return await query;
        }

        public async Task<List<DTOUsuario>> GetViewUsuarios()
        {
            var query = _context.Usuarios
                                .AsNoTracking()
                                .Include(c=>c.IdTipoUsuarioNavigation)
                                .Select(u => new DTOUsuario
                                {
                                    Nombres = u.Nombres,
                                    Apellidos = u.Apellidos,
                                    Legajo = u.Legajo,
                                    Telefono = u.Telefono,
                                    Calle = u.Calle,
                                    Numero = u.Numero,
                                    tipoUsuario = u.IdTipoUsuarioNavigation.Descripción,
                                    User = u.User,
                                    Activo = u.Activo == false ? "Inactivo" : "Activo"
                                }).ToListAsync();

            return await query;
        }

        public async Task<Usuario> GetUsuarioByID(long legajo)
        {
            var query = _context.Usuarios
                                .AsNoTracking()
                                .Where(c => c.Legajo == legajo)
                                .Select(u => new Usuario
                                {
                                    IdUsuarios = u.IdUsuarios,
                                    Nombres = u.Nombres,
                                    Apellidos = u.Apellidos,
                                    Legajo = u.Legajo,
                                    Telefono = u.Telefono,
                                    Calle = u.Calle,
                                    Numero = u.Numero,
                                    IdTipoUsuario = u.IdTipoUsuario,
                                    User = u.User,
                                    Activo = u.Activo,
                                }).FirstOrDefaultAsync();

            return await query;
        }

        public async Task<Usuario> GetUsuarioByUser(string user)
        {
            var query = _context.Usuarios
                                .AsNoTracking()
                                .Where(c => c.User == user)
                                .Select(u => new Usuario
                                {
                                    IdUsuarios = u.IdUsuarios,
                                    Nombres = u.Nombres,
                                    Apellidos = u.Apellidos,
                                    Legajo = u.Legajo,
                                    Telefono = u.Telefono,
                                    Calle = u.Calle,
                                    Numero = u.Numero,
                                    IdTipoUsuario = u.IdTipoUsuario,
                                    User = u.User,
                                    Activo = u.Activo,
                                }).FirstOrDefaultAsync();

            return await query;
        }

        public async Task<ResultadoBase> DeleteUsuario(int id)
        {
            ResultadoBase resultado = new ResultadoBase();
            var usuario = await _context.Usuarios.Where(c => c.Legajo.Equals(id)).FirstOrDefaultAsync();
            if (usuario != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El usuario se desactivo exitosamente.";
                usuario.Activo = false;
                _context.Update(usuario);
                await _context.SaveChangesAsync(/*_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName*/);
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al desactivar el cliente";
                return resultado;
            }

            return resultado;
        }
    }
}
