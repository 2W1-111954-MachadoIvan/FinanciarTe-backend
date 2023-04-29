using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceTiposUsuarios
    {
        Task<List<ComboBoxItemDto>> GetTipoUsuForComboBox();
    }
}
