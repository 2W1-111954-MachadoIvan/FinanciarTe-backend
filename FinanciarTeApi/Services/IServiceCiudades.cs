using FinanciarTeApi.Commands;

namespace FinanciarTeApi.Services
{
    public interface IServiceCiudades
    {
        Task<List<ComboBoxItemDto>> GetCiudadesForComboBox();
    }
}
