using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinanciarTeApi.Services
{
    public class ServiceTipoTransaccion : IServiceTipoTransaccion
    {
        private readonly FinanciarTeContext _context;

        public ServiceTipoTransaccion(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetTipoTransaccionForComboBox()
        {
            return await _context.TiposTransaccions.AsNoTracking().Select<TiposTransaccion, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
