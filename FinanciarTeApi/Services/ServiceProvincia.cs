using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceProvincia : IServiceProvincia
    {
        private readonly FinanciarTeContext _context;

        public ServiceProvincia(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetProvinciasForComboBox()
        {
            return await _context.Provincias.AsNoTracking().Select<Provincia, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
