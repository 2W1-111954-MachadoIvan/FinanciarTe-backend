using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceTiposEntidadFinanciera : IServiceTiposEntidadFinanciera
    {
        private readonly FinanciarTeContext _context;

        public ServiceTiposEntidadFinanciera(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetTipoEntFinForComboBox()
        {
            return await _context.TiposEntidadFinancieras.AsNoTracking().Select<TiposEntidadFinanciera, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
