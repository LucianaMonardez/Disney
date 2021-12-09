using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Disney.Models
{
    public class MovieOrSerie
    {
        [Key]
        public int IdPelicula { get; set; }
        public string ImagenPelicula { get; set; }
        [Required(ErrorMessage = "El campo Titulo no puede estar vacio")]
        [StringLength(100)]
        public string TituloPelicula { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        [Range(0, 5)]
        [Required(ErrorMessage = "El campo Valoracion no puede estar vacio")]
        public int Calificacion { get; set; }
        public int IdGenero { get; set; }
        [ForeignKey("IdGenero")]
        public Genre Genres { get; set; }
        public MovieOrSerie() { }

    }
}
