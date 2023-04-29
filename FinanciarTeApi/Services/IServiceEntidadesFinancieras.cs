using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceEntidadesFinancieras
    {
        Task<List<ComboBoxItemDto>> GetEntFinForComboBox();
    }
}
