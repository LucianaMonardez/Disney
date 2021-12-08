using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Ingresa tu nombre de usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Ingresa tu contraseña")]
        public string Password { get; set; }
    }
}
