using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciarTeApi.Services
{
    public class ServiceTiposUsuarios : IServiceTiposUsuarios
    {
        private readonly FinanciarTeContext _context;

        public ServiceTiposUsuarios(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetTipoUsuForComboBox()
        {
            return await _context.TiposUsuarios.AsNoTracking().Select<TiposUsuario, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
