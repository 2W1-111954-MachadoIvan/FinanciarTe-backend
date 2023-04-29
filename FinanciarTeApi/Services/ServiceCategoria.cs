using FinanciarTeApi.Commands;
using FinanciarTeApi.DataContext;
using FinanciarTeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinanciarTeApi.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        private readonly FinanciarTeContext _context;

        public ServiceCategoria(FinanciarTeContext context)
        {
            _context = context;
        }

        public async Task<List<ComboBoxItemDto>> GetCategoriasForComboBox()
        {
            return await _context.Categorias.AsNoTracking().Select<Categoria, ComboBoxItemDto>(x => x).ToListAsync();
        }
    }
}
