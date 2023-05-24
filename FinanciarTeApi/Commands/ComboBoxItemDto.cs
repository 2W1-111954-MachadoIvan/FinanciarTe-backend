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

        public static implicit operator ComboBoxItemDto(EntidadesFinanciera entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdEntidadFinanciera,
                descripcion = entity.Descripción
            };
        }

        public static implicit operator ComboBoxItemDto(Categoria entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdCategoria,
                descripcion = entity.Descripcion
            };
        }

        public static implicit operator ComboBoxItemDto(TiposUsuario entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdTipoUsuario,
                descripcion = entity.Descripción
            };
        }

        public static implicit operator ComboBoxItemDto(TiposEntidadFinanciera entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdTipoEntidad,
                descripcion = entity.Descripción
            };
        }

        public static implicit operator ComboBoxItemDto(TiposTransaccion entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.IdTipoTransaccion,
                descripcion = entity.Descripción
            };
        }

        public static implicit operator ComboBoxItemDto(Cliente entity)
        {
            return new ComboBoxItemDto
            {
                id = (int)entity.NroDni,
                descripcion = entity.Apellidos + " " +entity.Nombres
            };
        }
    }
}
