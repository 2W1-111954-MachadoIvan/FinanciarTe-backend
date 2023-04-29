using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinanciarTeApi.Services
{
    public class ServiceEntidadesFinancieras : IServiceEntidadesFinancieras
    {
        private readonly FinanciarTeContext _context;

        public ServiceEntidadesFinancieras(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetEntFinForComboBox()
        {
            return await _context.EntidadesFinancieras.AsNoTracking().Select<EntidadesFinanciera, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
