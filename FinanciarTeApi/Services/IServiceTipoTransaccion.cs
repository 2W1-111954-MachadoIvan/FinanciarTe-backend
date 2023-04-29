using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceTipoTransaccion
    {
        Task<List<ComboBoxItemDto>> GetTipoTransaccionForComboBox();
    }
}
