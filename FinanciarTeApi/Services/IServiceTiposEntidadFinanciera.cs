using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceTiposEntidadFinanciera
    {
        Task<List<ComboBoxItemDto>> GetTipoEntFinForComboBox();
    }
}
