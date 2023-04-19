using FinanciarTeApi.Models;

namespace FinanciarTeApi.Commands
{
    public class ComboBoxItemDto
    {
        public int id { get; set; }
        public int? idFk { get; set; }
        public string? descripcion { get; set; }
        public int? valor { get; set; }

        public static implicit operator ComboBoxItemDto(Provincia entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdProvincia,
                descripcion = entity.Provincia1
            };
        }

        public static implicit operator ComboBoxItemDto(Ciudade entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdCiudad,
                descripcion = entity.Ciudad
            };
        }
    }
}
