using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.DTOs.DTOUsuario
{


    public class DTOUsuario
    {

        public int? Id {  get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]

        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string? Apellido { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "La longitud debe estar entre 8 y 32 caracteres")]
        public string? Contrasenia { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "La longitud debe estar entre 8 y 32 caracteres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string? Rol { get; set; }



    }
}