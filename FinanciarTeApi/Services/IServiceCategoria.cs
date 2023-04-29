using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceCategoria
    {
        Task<List<ComboBoxItemDto>> GetCategoriasForComboBox();
    }
}
