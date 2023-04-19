using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceCiudades : IServiceCiudades
    {
        private readonly FinanciarTeContext _context;

        public ServiceCiudades(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetCiudadesForComboBox(int id)
        {
            return await _context.Ciudades.AsNoTracking().Where(x=> x.IdProvincia == id).Select<Ciudade, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
