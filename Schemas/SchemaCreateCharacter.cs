using System.ComponentModel.DataAnnotations;

namespace Disney.Schemas
{
    public class SchemaCreateCharacter
    {
        [Required(ErrorMessage = "El nombre del personaje es obligatorio")]
        [MaxLength(30, ErrorMessage = "Excediste los 30 caracteres")]
        public string NombrePersonaje { get; set; }

        public string ImagenPersonaje { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Historia { get; set; }

        [Required(ErrorMessage = "El numero de id de la pelicula es obligatorio")]
        public int IdPelicula { get; set; }
    }
}