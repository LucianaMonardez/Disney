using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Ingresa el nombre de usuario")]
        [StringLength(16, ErrorMessage = "Debe tener al menos 5 caracteres", MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Ingresa un email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingresa la contraseña")]
        [StringLength(16, ErrorMessage = "Debe tener al menos 5 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
   
}
