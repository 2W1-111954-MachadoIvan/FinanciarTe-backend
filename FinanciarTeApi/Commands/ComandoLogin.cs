using FinanciarTeApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FinanciarTeApi.Commands
{
    public class ComandoLogin
    {
        public long IdUsuario { get; set; }
        #region Command
        [Required(ErrorMessage = "El email es requerido")]
        public string User { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Pass { get; set; }
        #endregion

        #region Response
        public bool Activo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string tipoUsuario { get; set; }
        public string Token { get; set; }
        #endregion

        #region Resultado Base
        public string Message { set; get; } = null!;
        public bool Ok { set; get; }
        public string Error { get; set; }
        public int CodigoEstado { set; get; }
        #endregion

        public static implicit operator ComandoLogin(Usuario user)
        {
            if (user == null)
                return null;

            return new ComandoLogin
            {
                IdUsuario = user.IdUsuarios,
                Activo = user.Activo,
                User = user.User,
                Nombre = user.Nombres,
                Apellido = user.Apellidos,
                tipoUsuario = user.IdTipoUsuarioNavigation.Descripción
            };
        }
    }
}
