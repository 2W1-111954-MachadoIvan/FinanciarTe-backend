using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Results;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;

namespace FinanciarTeApi.Services
{
    public class ServiceDolar : IServiceDolar
    {
        private readonly HttpClient _httpClient;
        private readonly FinanciarTeContext _context;

        public ServiceDolar(FinanciarTeContext context)
        {
            _httpClient = new HttpClient();
            _context = context;

        }
        
        public async Task<List<DTODolarIndice>> GetValoresHistoricosDolar()
        {
            var query = _context.ViewHistoricoDolaIndices
                        .AsNoTracking()
                        .Select(g => new DTODolarIndice
                        {
                            Fecha = g.Fecha,
                            ValorDolar = g.ValorDolar,
                            ValorDolarBlue = g.ValorDolarBlue,
                            Indice = g.Indice,
                        });

            return await query.ToListAsync();
        }

        public async Task<DTODolarIndice> GetUltimoValorDolar()
        {
            var maxFecha = await _context.ViewHistoricoDolaIndices.MaxAsync(c => c.Fecha);

            var query = _context.ViewHistoricoDolaIndices
                        .AsNoTracking()
                        .Where(c=>c.Fecha == maxFecha)
                        .Select(g => new DTODolarIndice
                        {
                            Fecha = g.Fecha,
                            ValorDolar = g.ValorDolar,
                            ValorDolarBlue = g.ValorDolarBlue,
                            Indice = g.Indice,
                        })
                        .FirstOrDefaultAsync();

            return await query;
        }

    }
}
