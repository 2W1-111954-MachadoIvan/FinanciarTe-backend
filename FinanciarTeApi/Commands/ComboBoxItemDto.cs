using FinanciarTeApi.Models;

namespace FinanciarTeApi.Commands
{
    public class ComboBoxItemDto
    {
        public int Id { get; set; }
        public int? IdProvincia { get; set; }
        public string? Descripcion { get; set; }

        public static implicit operator ComboBoxItemDto(Provincia entity)
        {
            return new ComboBoxItemDto
            {
                Id = (int)entity.IdProvincia,
                Descripcion = entity.Provincia1
            };
        }

        public static implicit operator ComboBoxItemDto(Ciudade entity)
        {
            return new ComboBoxItemDto
            {
                Id = (int)entity.IdCiudad,
                Descripcion = entity.Ciudad,
                IdProvincia = (int?)entity.IdProvincia
            };
        }
    }
}
