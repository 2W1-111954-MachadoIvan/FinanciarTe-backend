﻿using System.ComponentModel.DataAnnotations;

namespace FinanciarTeApi.Commands
{
    public class ComandoPutUsuario
    {
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La calle es requerido.")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "El numero es requerido.")]
        public long Numero { get; set; }
        [Required(ErrorMessage = "El telefono es requerido.")]
        public long Telefono { get; set; }
        [Required(ErrorMessage = "El legajo es requerido.")]
        public long Legajo { get; set; }
        [Required(ErrorMessage = "El email es requerido.")]
        public string User { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string PassActual { get; set; }
        public string PassNueva { get; set; }
    }
}