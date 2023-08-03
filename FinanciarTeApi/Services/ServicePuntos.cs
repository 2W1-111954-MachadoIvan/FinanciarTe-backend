using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServicePuntos : IServicePuntos
    {
        private readonly FinanciarTeContext _context;

        public ServicePuntos(FinanciarTeContext context)
        {
            _context = context;

        }

        public async Task<List<DTOPuntos>> GetHistoricoPuntos()
        {
            var query = _context.ViewHistoricoPuntos
                        .AsNoTracking()
                        .Select(g => new DTOPuntos
                        {
                            Dni = g.Dni,
                            Cliente = g.Cliente,
                            Fecha = g.Fecha,
                            Detalle = g.Detalle,
                            Descripcion = g.Descripción,
                            Puntos = g.Puntos
                        });

            return await query.ToListAsync();
        }

        public async Task<List<DTOPuntos>> GetHistoricoPuntosByCliente(long id)
        {
            var query = _context.ViewHistoricoPuntos
                        .AsNoTracking()
                        .Where(c => c.Dni == id)
                        .Select(g => new DTOPuntos
                        {
                            Dni = g.Dni,
                            Cliente = g.Cliente,
                            Fecha = g.Fecha,
                            Detalle = g.Detalle,
                            Descripcion = g.Descripción,
                            Puntos = g.Puntos
                        });

            return await query.ToListAsync();
        }
    }
}
