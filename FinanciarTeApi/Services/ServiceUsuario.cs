using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
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
    }
}
