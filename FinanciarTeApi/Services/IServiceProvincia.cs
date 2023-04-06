using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceProvincia
    {
        Task<List<ComboBoxItemDto>> GetProvinciasForComboBox();
    }
}
