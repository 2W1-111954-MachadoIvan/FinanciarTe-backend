using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceReportes : IServiceReportes
    {
        private readonly FinanciarTeContext _context;

        public ServiceReportes(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<DTOResumenPrestamos>> GetResumenPrestamos()
        {
            var query = _context.ViewResumenPrestamos
                        .AsNoTracking()
                        .OrderByDescending(c => c.CantidadDePrestamos)
                        .Select(g => new DTOResumenPrestamos
                        {
                            Nombre = g.Nombre,
                            Cliente = g.Cliente,
                            CantidadDePrestamos = g.CantidadDePrestamos,
                            PrestamosPendientes = g.PrestamosPendientes,
                            PrestamosCancelados = g.PrestamosCancelados,
                            PrestamosRefinanciados = g.PrestamosRefinanciados,
                            CuotasVencidas = g.CuotasVencidas,
                            TotalDeCuotas = g.TotalDeCuotas,
                            PorcentajeCumplCuotas = g.PorcentajeCumplCuotas
                        });

            return await query.ToListAsync();
        }

        public async Task<List<DTODolarIndice>> GetMaxMinDolarIndice()
        {
            var maxFecha = await _context.ViewHistoricoDolaIndices.MaxAsync(c => c.Fecha);
            var minFecha = await _context.ViewHistoricoDolaIndices.MinAsync(c => c.Fecha);

            var query = _context.ViewHistoricoDolaIndices
                        .AsNoTracking()
                        .Where(c => c.Fecha == maxFecha || c.Fecha == minFecha)
                        .Select(g => new DTODolarIndice
                        {
                            Fecha = g.Fecha,
                            ValorDolar = g.ValorDolar,
                            ValorDolarBlue = g.ValorDolarBlue,
                            Indice = g.Indice,
                        })
                        .ToListAsync();

            return await query;
        }

        public async Task<List<DTODolarIndice>> GetValoresHistoricosDolar()
        {
            var query = _context.ViewHistoricoDolaIndices
                        .AsNoTracking()
                        .OrderBy(c => c.Fecha)
                        .Select(g => new DTODolarIndice
                        {
                            Fecha = g.Fecha,
                            ValorDolar = g.ValorDolar,
                            ValorDolarBlue = g.ValorDolarBlue,
                            Indice = g.Indice,
                        });

            return await query.ToListAsync();
        }
        
        public async Task<List<DTORecaudacion>> GetRecaudacionMensual()
        {
            var query = _context.ViewRecaudacionMensuals
                        .AsNoTracking()
                        .Select(g => new DTORecaudacion
                        {
                            RecaudacionMensual = g.RecaudaciónMensual,
                            RecaudacionEsperada = g.RecaudaciónEsperada
                        });

            return await query.ToListAsync();
        }

        public async Task<List<DTOBalance>> GetBalance()
        {
            var query = _context.ViewBalances
                        .AsNoTracking()
                        .Select(g => new DTOBalance
                        {
                            idEntidadFinanciera = g.IdEntidadFinanciera,
                            Descripcion = g.Descripción,
                            MontoInicial = g.MontoInicial,
                            MontoActual = g.MontoActual
                        });

            return await query.ToListAsync();
        }

        public async Task<List<DTOCuotasMesEnCurso>> GetCuotasMesEnCurso()
        {
            var query = _context.ViewCuotasMesEnCursos
                        .AsNoTracking()
                        .Select(g => new DTOCuotasMesEnCurso
                        {
                            Descripcion = g.Descripcion,
                            Cantidad = g.Cantidad
                        });

            return await query.ToListAsync();
        }
    }
}
