using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disney.Models
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdPersonaje { get; set; }

        public string ImagenPersonaje { get; set; }

        [Required(ErrorMessage = "El campo Nombre no puede estar vacio")]
        [StringLength(30)]
        public string NombrePersonaje { get; set; }

        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Historia { get; set; }

        public int IdPelicula { get; set; }

        [ForeignKey("IdPelicula")]
        public MovieOrSerie MovieOrSerie { get; set; }
    }
}