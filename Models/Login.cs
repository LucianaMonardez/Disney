using System.ComponentModel.DataAnnotations;

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